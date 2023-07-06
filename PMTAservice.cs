using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scoredlist.NET.Utilities;
using System.Data;
using Insight.Database;
using EASendMail;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using SmtpClient = EASendMail.SmtpClient;

namespace EmailEngineTesting
{

    public class PMTAservice
    {
        public class EmailProcessorModel
        {

            public SmtpMail oMail { get; set; }
            public SmtpServer oServer { get; set; }
            public string PURLresponse { get; set; }
            public string[] responseArray { get; set; }
            public string OutboundDomainName { get; set; }
            public int EmailBatch_ID { get; set; }
            public string SubjectLine { get; set; }
            public string PURL { get; set; }
            public string ResponseCode { get; set; }
            public string PromoCode { get; set; }
            public string EmailAddress { get; set; }
        }

        public class EngineSettings
        {
            public int EmailServiceProviderGroup_ID { get; set; }
            public string OutboundDomainName { get; set; }
            public string OutboundUsername { get; set; }
            public string OutboundPP { get; set; }
            public string OutboundServerAddress { get; set; }
            public int MinutesLeftInWeek { get; set; }
            public int HourlyEmailLimit { get; set; }
            public int FailedEmailCount { get; set; }
        }

        public class RecipientModel
        {
            public int EmailBatch_ID { get; set; }
            public string FromEmailAddress { get; set; }
            public string ReplyToEmailAddress { get; set; }
            public string SubjectLine { get; set; }
            public string TemplateURL { get; set; }
            public string Unsubscribe { get; set; }
            public bool ClickTracking { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string ResponseCode { get; set; }
            public string PromoCode { get; set; }
            public string EmailAddress { get; set; }
            public string result { get; set; }
            public string filter { get; set; }
            public decimal OfferPayment { get; set; }
        }

        public static async Task SendPMTAAsync(int EmailServiceProvider_ID, DateTime DropDate, bool Realtime)
        {
            IEnumerable<EngineSettings> ES;
            int SendCycle = 0;
            int DropIndex = 0;
            int WeekCount = 0;
            int CurrentEmailBatchID = 0;
            decimal SendIntervalSeconds = 600;
            DateTime AppStartTimestamp = DateTime.Now;
            string DataCenterEmailEngine = ConfigurationManager.ConnectionStrings["DataCenterEmailEngine"].ConnectionString;

            using IDbConnection EmailDrop = new SqlConnection(DataCenterEmailEngine);

            CurrentEmailBatchID = await EmailDrop.ExecuteScalarAsync<int>("WeeklyEmailBatches_GetNext",
                new { DropDate, Realtime, EmailServiceProvider_ID, Processor_ID = 1 }, commandTimeout: 180);

            WeekCount = await EmailDrop.ExecuteScalarAsync<int>("WeeklyEmailBatchRecipients_GetCountV2", new { EmailServiceProvider_ID, Realtime }, commandTimeout: 180);


            IEnumerable<RecipientModel> TheDrop = await EmailDrop.QueryAsync<RecipientModel>("WeeklyEmailBatchRecipients_GetV3",
                new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID, Realtime }, commandTimeout: 180);

            using IDbConnection EmailEngineSettings = new SqlConnection(DataCenterEmailEngine);

            ES = EmailEngineSettings.Query<EngineSettings>("WeeklyEmailEngineSettings_Get", new { EmailServiceProvider_ID }, commandTimeout: 180).ToList();

            if (ES.Any() && CurrentEmailBatchID > 0)
            {
                int MinutesLeftInWeekAtStart = ES.FirstOrDefault().MinutesLeftInWeek;
                if (WeekCount > 0) SendIntervalSeconds = (MinutesLeftInWeekAtStart / ((decimal)WeekCount / ES.Count())) * 60;
                if (SendIntervalSeconds > 21600) SendIntervalSeconds = 21600;
                if (Realtime) SendIntervalSeconds = 2;

                Console.WriteLine("Send Interval in Seconds: " + ((int)SendIntervalSeconds).ToString());
                Console.WriteLine("Email Servers: " + ES.Count().ToString());
                Console.WriteLine("Week Count: " + WeekCount.ToString());
                Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());
                Console.WriteLine("Batch: " + CurrentEmailBatchID.ToString());
                Console.WriteLine("Drop Count: " + TheDrop.Count().ToString());

                while (ES.Any() && CurrentEmailBatchID > 0)
                {
                    DateTime StartTime = DateTime.Now;
                    IEnumerable<EmailProcessorModel> Emails = new List<EmailProcessorModel>();

                    foreach (EngineSettings EngineSetting in ES)
                    {

                        if (DropIndex >= TheDrop.Count())
                        {
                            using IDbConnection BatchStatus = new SqlConnection(DataCenterEmailEngine);
                            BatchStatus.Execute("WeeklyEmailBatchEnd_Save", new { EmailBatch_ID = CurrentEmailBatchID, EmailServiceProvider_ID, Processor_ID = 1 });

                            CurrentEmailBatchID = BatchStatus.Query<int>("WeeklyEmailBatches_GetNext",
                                new { DropDate, Realtime, EmailServiceProvider_ID, Processor_ID = 1 }, commandTimeout: 180).Single();

                            if (CurrentEmailBatchID == 0) return;

                            Console.WriteLine("Batch: " + CurrentEmailBatchID.ToString());

                            TheDrop = BatchStatus.Query<RecipientModel>("WeeklyEmailBatchRecipients_GetV3",
                                new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID, Realtime }, commandTimeout: 180).ToList();

                            Console.WriteLine("Drop Count: " + TheDrop.Count().ToString());

                            DropIndex = 0;
                            if (!TheDrop.Any()) return;
                        }
                        RecipientModel recipient = TheDrop.ElementAt(DropIndex);
                        string result = recipient.result.ToLower();
                        if (result == "valid" || result == "neutral")
                        {

                            string bidenc = CommonUtilities.StandardEncryptText(recipient.EmailBatch_ID + "//" + recipient.EmailAddress);
                            string UnSub = recipient.Unsubscribe.Replace("##promocode##", recipient.PromoCode).Replace("##responsecode##", recipient.ResponseCode).Replace("##emailaddress##", recipient.EmailAddress);
                            string Google = "|value1|:|value2|:|value3|:|value4|".Replace("|value1|", recipient.EmailBatch_ID.ToString()).Replace("|value2|", recipient.ResponseCode).Replace("|value3|", DateTime.Now.ToString("MM/dd/yyyy")).Replace("|value4|", "Dlinks");

                            EmailProcessorModel Email = new()
                            {
                                oServer = new SmtpServer(EngineSetting.OutboundServerAddress)
                                {
                                    User = EngineSetting.OutboundUsername,
                                    Password = EngineSetting.OutboundPP,
                                    ConnectType = SmtpConnectType.ConnectTryTLS,
                                    Port = 2525
                                },
                                PURL = recipient.TemplateURL.ToLower().Replace("##responsecode##", recipient.ResponseCode).Replace("##emailaddress##", recipient.EmailAddress).Replace("##bidenc##", bidenc).Replace("##bid##", recipient.EmailBatch_ID.ToString()),
                                EmailBatch_ID = recipient.EmailBatch_ID,
                                ResponseCode = recipient.ResponseCode,
                                PromoCode = recipient.PromoCode,
                                EmailAddress = recipient.EmailAddress,
                                OutboundDomainName = EngineSetting.OutboundDomainName,
                                oMail = new SmtpMail("ES-E1582190613-00899-DU956331B9EA29VA-51T11E9DD8A7D591")
                                {
                                    //Bcc = "buddy@buddymurphy.com",
                                    From = recipient.FromEmailAddress.Replace("@offersdirect.com", "@" + EngineSetting.OutboundDomainName).Replace("@mailgun.offersdirect.com", "@" + EngineSetting.OutboundDomainName),
                                    To = CommonUtilities.Capitalize(recipient.FirstName) + " " + CommonUtilities.Capitalize(recipient.LastName) + " <" + recipient.EmailAddress.ToLower() + ">",
                                    ReplyTo = recipient.ReplyToEmailAddress,
                                    Subject = recipient.SubjectLine.Replace("##firstname##", CommonUtilities.Capitalize(recipient.FirstName)).Replace("##emailaddress##", recipient.EmailAddress).Replace("##offerpayment##", recipient.OfferPayment.ToString("C0"))
                                }
                            };
                            Email.oMail.Headers.Add("List-Unsubscribe", $" <{UnSub}>");
                            Emails = Emails.Concat(new[] { Email });
                            DropIndex++;
                        }
                        else DropIndex++;
                    };

                    var sendEmailTasks = Emails.Select(async Email =>
                    {
                        using WebClient wclient = new();
                        try
                        {
                            Email.PURLresponse = wclient.DownloadString(Email.PURL);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("URL Failed");
                            Console.WriteLine("Batch ID: " + Email.EmailBatch_ID);
                            Console.WriteLine("PURL: " + Email.PURL);
                            Console.WriteLine("Outbound: " + Email.OutboundDomainName);
                            Console.WriteLine("Recipient: " + Email.EmailAddress.ToLower());
                            Console.WriteLine("Error: " + ex.Message);
                            Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());

                            Email.PURLresponse = "";
                        }

                        Email.responseArray = Email.PURLresponse.Split(new string[] { "###HTML-Text###" }, StringSplitOptions.None);
                        if (Email.responseArray.Length == 2)
                        {
                            Email.responseArray[0] = Email.responseArray[0].Replace("</body>", "<img src='https://www.offersdirect.com/image/ODCopyright/Copyright_##responsecode##_##emailbatch##' /></body>".Replace("##responsecode##", Email.ResponseCode).Replace("##emailbatch##", Email.EmailBatch_ID.ToString()));
                            Email.oMail.TextBody = Email.responseArray[1];
                            Email.oMail.HtmlBody = Email.responseArray[0];

                            Console.WriteLine("Emailed -> " + Email.EmailAddress + " ResponseCode -> " + Email.ResponseCode + " Subjectline -> " + Email.SubjectLine);

                            //try
                            //{
                            //    SmtpClient oSmtp = new SmtpClient();
                            //    //await oSmtp.SendMailAsync(Email.oServer, Email.oMail);
                            //
                            //    using (IDbConnection db = new SqlConnection(connectionString))
                            //    {
                            //        await db.ExecuteSqlAsync(
                            //            "EXEC SentMessage_Save @EmailBatch_ID, @ResponseCode, @EmailAddress, @MessageID, @MessageStatus_ID",
                            //            new
                            //            {
                            //                EmailBatch_ID = Email.EmailBatch_ID,
                            //                ResponseCode = Email.ResponseCode,
                            //                EmailAddress = Email.EmailAddress.ToLower(),
                            //                MessageID = Email.oMail.MessageID,
                            //                MessageStatus_ID = 1
                            //            });
                            //    }
                            //}

                            //catch (Exception e)
                            //{
                            //    Console.WriteLine("Error: " + e.Message);
                            //    Console.WriteLine("Outbound: " + Email.OutboundDomainName);
                            //    Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());
                            //
                            //
                            //}
                        }
                    });
                    await Task.WhenAll(sendEmailTasks);
                    SendCycle++;
                    using (IDbConnection EmailEngineSettingsNew = new SqlConnection(DataCenterEmailEngine))
                    {
                        int currentESCount = ES.Count();
                        ES = EmailEngineSettings.Query<EngineSettings>("WeeklyEmailEngineSettings_Get", new { EmailServiceProvider_ID }, commandTimeout: 180).ToList();

                        ES = ES.Where(x => x.HourlyEmailLimit > SendCycle);

                        Console.WriteLine($"Server Count Checked, currently {ES.Count()}");
                        Console.WriteLine($"Time: {DateTime.Now.ToLongTimeString()}");
                        if (ES.Count() != currentESCount && ES.Any())
                        {
                            MinutesLeftInWeekAtStart = ES.First().MinutesLeftInWeek;
                            Console.WriteLine($"Server Count Changed from {currentESCount} to {ES.Count()}");
                            SendIntervalSeconds = (MinutesLeftInWeekAtStart / (WeekCount / ES.Count())) * 60;
                            if (SendIntervalSeconds > 21600) SendIntervalSeconds = 21600;
                            Console.WriteLine($"Send Interval in Seconds: {(int)SendIntervalSeconds}");
                            Console.WriteLine($"Email Servers: {ES.Count()}");
                        }
                    }
                    if (AppStartTimestamp.AddHours(24) < DateTime.Now)
                    {
                        AppStartTimestamp = DateTime.Now;
                        SendCycle = 0;
                        using IDbConnection WeeklyCountRefresh = new SqlConnection("DataCenterEmailEngine");
                        WeekCount = WeeklyCountRefresh.Query<int>("WeeklyEmailBatchRecipients_GetCountV2",
                            new { EmailServiceProvider_ID, Realtime }, commandTimeout: 180).FirstOrDefault();
                        Console.WriteLine($"Weekly Count Refresh: {WeekCount}");
                        SendIntervalSeconds = (MinutesLeftInWeekAtStart / ((decimal)WeekCount / ES.Count())) * 60;
                        if (SendIntervalSeconds > 21600) SendIntervalSeconds = 21600;
                        Console.WriteLine($"Send Interval in Seconds: {(int)SendIntervalSeconds}");
                    }

                    DateTime finishtime = DateTime.Now;
                    double diffInSeconds = (finishtime - StartTime).TotalSeconds;
                    double IntervalDelta = (double)SendIntervalSeconds - diffInSeconds;
                    int Interval = (int)(IntervalDelta * 1000.00);
                    if (Interval > 0 && DropIndex < TheDrop.Count()) Thread.Sleep(Interval);
                }
            }
            else
            {
                Console.WriteLine("No Active Servers......................................");
                Thread.Sleep(50000);
                using IDbConnection BatchStatus = new SqlConnection(DataCenterEmailEngine);
                BatchStatus.Execute("WeeklyEmailBatchEnd_Save", new { EmailBatch_ID = CurrentEmailBatchID, EmailServiceProvider_ID, Processor_ID = 1 });
            }
        }
    }
}
