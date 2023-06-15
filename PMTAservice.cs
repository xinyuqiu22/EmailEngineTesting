using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Insight.Database;
using EASendMail;

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

        public static void sendPMTA(int EmailServiceProvider_ID, DateTime DropDate, bool Realtime)
        {
            
            IEnumerable<EngineSettings> ES;
            int SendCycle = 0;
            int DropIndex = 0;
            int WeekCount = 0;
            int CurrentEmailBatchID = 0;
            decimal SendIntervalSeconds = 600;
            DateTime AppStartTimestamp = DateTime.Now;
            string connectionString = ConfigurationManager.ConnectionStrings["DataCenterEmailEngine"].ConnectionString;

            using (IDbConnection EmailDrop = new SqlConnection(connectionString))
            {


                CurrentEmailBatchID = EmailDrop.QuerySql<int>(
                    "EXEC WeeklyEmailBatches_GetNext @DropDate, @Realtime, @EmailServiceProvider_ID, @Processor_ID",
                    new { DropDate, Realtime, EmailServiceProvider_ID, Processor_ID = 1 }).Single();

                WeekCount = EmailDrop.QuerySql<int>(
                    "EXEC WeeklyEmailBatchRecipients_GetCountV2 @EmailServiceProvider_ID, @Realtime",
                    new { EmailServiceProvider_ID, Realtime }).Single();


                IEnumerable<RecipientModel> TheDrop = EmailDrop.QuerySql<RecipientModel>(
                    "EXEC WeeklyEmailBatchRecipients_GetV3 @EmailServiceProvider_ID, @EmailBatch_ID, @Realtime",
                    new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID, Realtime }).ToList();

                using (IDbConnection EmailEngineSettings = new SqlConnection(connectionString))
                {
                    ES = EmailEngineSettings.QuerySql<EngineSettings>(
                        "EXEC WeeklyEmailEngineSettings_Get @EmailServiceProvider_ID",
                        new { EmailServiceProvider_ID }).ToList();

                    if (ES.Count() > 0 && CurrentEmailBatchID > 0)
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

                        while(ES.Count() > 0 && CurrentEmailBatchID > 0)
                        {
                            DateTime StartTime = DateTime.Now;
                            IEnumerable<EmailProcessorModel> Emails = new List<EmailProcessorModel>();


                            Parallel.ForEach(ES, EngineSetting =>
                            {

                                if (DropIndex >= TheDrop.Count())
                                {
                                    using (IDbConnection BatchStatus = new SqlConnection(connectionString)) 
                                    {
                                        BatchStatus.ExecuteSql(
                                            "EXEC WeeklyEmailBatchEnd_Save @EmailBatch_ID, @EmailServiceProvider_ID, @Processor_ID",
                                            new { EmailBatch_ID = CurrentEmailBatchID, EmailServiceProvider_ID, Processor_ID = 1 });
                                        
                                        CurrentEmailBatchID = BatchStatus.QuerySql<int>(
                                            "EXEC WeeklyEmailBatches_GetNext @DropDate, @Realtime, @EmailServiceProvider_ID, @Processor_ID",
                                            new { DropDate, Realtime, EmailServiceProvider_ID, Processor_ID = 1 }).Single();

                                        if (CurrentEmailBatchID == 0) return;

                                        Console.WriteLine("Batch: " + CurrentEmailBatchID.ToString());
                                        TheDrop = EmailDrop.QuerySql<RecipientModel>(
                                            "EXEC WeeklyEmailBatchRecipients_GetV3 @EmailServiceProvider_ID, @EmailBatch_ID, @Realtime",
                                            new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID, Realtime }).ToList();

                                        Console.WriteLine("Drop Count: " + TheDrop.Count().ToString());

                                        DropIndex = 0;
                                        if (TheDrop.Count() == 0) return;
                                    }
                                }
                                RecipientModel recipient = TheDrop.ElementAt(DropIndex);
                                string result = recipient.result.ToLower();
                                if (result == "valid" || result == "neutral")
                                {

                                }


                            });
                        }
                    }
                }

            }

        }
    }
}
