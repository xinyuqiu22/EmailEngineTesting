using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scoredlist.NET.Utilities;
using System.Data;
using Insight.Database;
using RestSharp;
using RestSharp.Authenticators;
using SmtpClient = EASendMail.SmtpClient;
using System.Net;
using Newtonsoft.Json;

namespace EmailEngineTesting
{
    public class MailGunService
    {
        public class MailCallResult
        {
            public string id { get; set; }
            public string message { get; set; }
        }

        public class EmailProcessorModel
        {
            public RestClient client { get; set; }
            public RestRequest request { get; set; }
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
        public static async Task SendMailgunAsync(int EmailServiceProvider_ID, DateTime DropDate)
        {
            IEnumerable<EngineSettings> ES; 
            int SendCycle = 0;
            int DropIndex = 0;
            int CurrentEmailBatchID = 0;
            decimal WeekCount = 0;
            DateTime AppStartTimestamp = DateTime.Now;
            string DataCenterEmailEngine = ConfigurationManager.ConnectionStrings["DataCenterEmailEngine"].ConnectionString;

            using IDbConnection EmailDrop = new SqlConnection(DataCenterEmailEngine);
            CurrentEmailBatchID = await EmailDrop.ExecuteScalarAsync<int>( "EmailBatches_GetNextV2", new { DropDate, Realtime = 0, EmailServiceProvider_ID, Processor_ID = 2 }, commandTimeout: 180);

            WeekCount = await EmailDrop.ExecuteScalarAsync<int>( "WeeklyEmailBatchRecipients_GetMailgunCount", new { EmailServiceProvider_ID, DropDate }, commandTimeout: 180);

            IEnumerable<RecipientModel> TheDrop = await EmailDrop.QueryAsync<RecipientModel>( "WeeklyEmailBatchRecipients_GetMailgunV2", 
                new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID }, commandTimeout: 180);

            using IDbConnection EmailEngineSettings = new SqlConnection(DataCenterEmailEngine);

            ES = await EmailEngineSettings.QueryAsync<EngineSettings>( "WeeklyEmailEngineSettings_GetMailgunReserved", new { EmailServiceProvider_ID }, commandTimeout: 180);

            if (ES.Any() && CurrentEmailBatchID > 0)
            {
                decimal SendIntervalSeconds = 1.5m;

                Console.WriteLine("Send Interval in Seconds: " + (SendIntervalSeconds).ToString());
                Console.WriteLine("Email Servers: " + ES.Count().ToString());
                Console.WriteLine("Daily Count: " + WeekCount.ToString());
                Console.WriteLine("Drop Count: " + TheDrop.Count().ToString());
                Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());
                Console.WriteLine("Batch: " + CurrentEmailBatchID.ToString());

                while (ES.Any() && CurrentEmailBatchID > 0)
                {
                    DateTime Starttime = DateTime.Now;
                    IEnumerable<EmailProcessorModel> Emails = new List<EmailProcessorModel>();

                    foreach (EngineSettings EngineSetting in ES)
                    {
                        if (DropIndex >= TheDrop.Count())
                        {
                            using IDbConnection BatchStatus = new SqlConnection(DataCenterEmailEngine);
                            BatchStatus.Execute("WeeklyEmailBatchEnd_Save",
                                new { EmailBatch_ID = CurrentEmailBatchID, EmailServiceProvider_ID, Processor_ID = 2 });

                            CurrentEmailBatchID = BatchStatus.Query<int>("EmailBatches_GetNextV2", new { DropDate, Realtime = 0, EmailServiceProvider_ID, Processor_ID = 2 }, commandTimeout: 180).Single();

                            TheDrop = BatchStatus.Query<RecipientModel>("WeeklyEmailBatchRecipients_GetMailgunV2", new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID }, commandTimeout: 180).ToList();

                            Console.WriteLine("Email Servers: " + ES.Count().ToString());
                            Console.WriteLine("Drop Count: " + TheDrop.Count().ToString());
                            Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());
                            Console.WriteLine("Batch: " + CurrentEmailBatchID.ToString());
                            DropIndex = 0;
                            if (!TheDrop.Any()) return;
                        }
                        RecipientModel recipient = TheDrop.ElementAt(DropIndex);
                        string result = recipient.result.ToLower();
                        if (result == "valid")
                        {
                            string bidenc = CommonUtilities.StandardEncryptText(recipient.EmailBatch_ID + "//" + recipient.EmailAddress);
                            string UnSub = recipient.Unsubscribe.Replace("##promocode##", recipient.PromoCode).Replace("##responsecode##", recipient.ResponseCode).Replace("##emailaddress##", recipient.EmailAddress);
                            string Google = "|value1|:|value2|:|value3|:|value4|".Replace("|value1|", recipient.EmailBatch_ID.ToString()).Replace("|value2|", recipient.ResponseCode).Replace("|value3|", DateTime.Now.ToString("MM/dd/yyyy")).Replace("|value4|", "Dlinks");

                            var clientOptions = new RestClientOptions(EngineSetting.OutboundServerAddress)
                            {
                                Authenticator = new HttpBasicAuthenticator("api", EngineSetting.OutboundPP),
                                BaseUrl = new Uri(EngineSetting.OutboundServerAddress)
                            };

                            EmailProcessorModel Email = new()
                            {
                                PURL = recipient.TemplateURL.ToLower().Replace("##responsecode##", recipient.ResponseCode).Replace("##emailaddress##", recipient.EmailAddress).Replace("##bidenc##", bidenc).Replace("##bid##", recipient.EmailBatch_ID.ToString()),
                                EmailBatch_ID = recipient.EmailBatch_ID,
                                ResponseCode = recipient.ResponseCode,
                                PromoCode = recipient.PromoCode,
                                EmailAddress = recipient.EmailAddress,
                                OutboundDomainName = EngineSetting.OutboundDomainName,
                                //new RestClient make Authenticator 
                                client = new RestClient(clientOptions),
                                request = new RestRequest()
                                {
                                    Resource = "{domain}/messages",
                                    Method = Method.Post
                                }
                            };

                            string SubjectLine = recipient.SubjectLine.Replace("##firstname##", CommonUtilities.Capitalize(recipient.FirstName))
                                   .Replace("##emailaddress##", recipient.EmailAddress)
                                   .Replace("##offerpayment##", recipient.OfferPayment.ToString("C0"));

                            Email.request.AddParameter("domain", EngineSetting.OutboundDomainName, ParameterType.UrlSegment);
                            Email.request.AddParameter("from", recipient.FromEmailAddress.Replace("@offersdirect.com", "@" + EngineSetting.OutboundDomainName));
                            Email.request.AddParameter("to", CommonUtilities.Capitalize(recipient.FirstName) + " " + CommonUtilities.Capitalize(recipient.LastName) + " <" + recipient.EmailAddress.ToLower() + ">");

                            Email.request.AddParameter("subject", SubjectLine);
                            Email.request.AddParameter("o:dkim", "yes");
                            Email.request.AddParameter("o:tracking-opens", "yes");
                            Email.request.AddParameter("o:tag", recipient.EmailAddress.ToLower().Split('@')[1]);
                            Email.request.AddParameter("o:tag", "STO_enabled");

                            Email.request.AddParameter("h:List-Unsubscribe", UnSub);
                            Email.request.AddParameter("h:Reply-To", recipient.ReplyToEmailAddress);
                            Emails = Emails.Concat(new[] { Email });
                            DropIndex++;
                        }
                    };

                    var SendEmailTasks = Emails.Select(async (Email) =>
                    {
                        using WebClient wclient = new();
                        try
                        {
                            Email.PURLresponse = await wclient.DownloadStringTaskAsync(Email.PURL);
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
                            Email.request.AddParameter("html", Email.responseArray[0]);
                            Email.request.AddParameter("text", Email.responseArray[1]);

                            try
                            {
                                Console.WriteLine("Emailed -> " + Email.EmailAddress + " ResponseCode -> " + Email.ResponseCode + " Subjectline -> " + Email.SubjectLine);
                                //RestResponse resp = await Email.client.ExecuteAsync(Email.request);
                                //if (resp.IsSuccessful)
                                //{
                                //    string results = resp.Content.ToString();
                                //    Console.WriteLine("Emailed -> " + Email.EmailAddress + " Subjectline -> " + Email.SubjectLine);
                                //    //RestResponse resp = await Email.client.ExecuteAsync(Email.request);
                                //    //MailCallResult ResultsOB = JsonConvert.DeserializeObject<MailCallResult>(results);
                                //    //using (IDbConnection SentMessage = new SqlConnection(connectionString))
                                //    //{
                                //    //    //SentMessage.CommandTimeout = 180000;
                                //    //    SentMessage.ExecuteSql(
                                //    //        "EXEC SentMessage_Save @EmailBatch_ID, @ResponseCode, @EmailAddress, @MessageID, @MessageStatus_ID",
                                //    //        new { Email.EmailBatch_ID, Email.ResponseCode, EmailAddress = Email.EmailAddress.ToLower(), MessageID = ResultsOB.id, MessageStatus_ID = 1 });
                                //    //}
                                //}
                                //else
                                //{
                                //    Console.WriteLine("Failed: " + resp.Content);
                                //    ES = ES.Where(x => x.OutboundDomainName != Email.OutboundDomainName);
                                //    Console.WriteLine("Email Servers: " + ES.Count());
                                //}

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: " + e.Message);
                                Console.WriteLine("Outbound: " + Email.OutboundDomainName);
                                Console.WriteLine("Time: " + DateTime.Now.ToLongTimeString());
                            }
                        }
                    }).ToList();
                    await Task.WhenAll(SendEmailTasks);

                    SendCycle++;
                    int currentESCount = ES.Count();
                    ES = ES.Where(x => x.HourlyEmailLimit > SendCycle);

                    if (ES.Count() != currentESCount)
                    {
                        Console.WriteLine($"Server Count Changed from {currentESCount} to {ES.Count()}");
                        SendIntervalSeconds = 1.5m;
                        Console.WriteLine($"Send Interval in Seconds: {SendIntervalSeconds}");
                        Console.WriteLine($"Email Servers: {ES.Count()}");
                    }

                    DateTime Finishtime = DateTime.Now;
                    double DiffInSeconds = (Finishtime - Starttime).TotalSeconds;
                    double IntervalDelta = (double)SendIntervalSeconds - DiffInSeconds;
                    int Interval = (int)(IntervalDelta * 1000.00);
                    if (Interval > 0) await Task.Delay(Interval);
                }
                using (IDbConnection BatchStatus = new SqlConnection(DataCenterEmailEngine))
                {
                    BatchStatus.Execute("WeeklyEmailBatchStartEndPartial_Save", new { EmailBatch_ID = CurrentEmailBatchID, EmailServiceProvider_ID, Processor_ID = 2 });
                }
            }
        }

    }
}
