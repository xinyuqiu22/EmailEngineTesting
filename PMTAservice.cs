using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using Insight.Database;
using System.Web.Http;
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
            List<EngineSettings> ES;
            int SendCycle = 0;
            int DropIndex = 0;
            decimal WeekCount = 0;
            int CurrentEmailBatchID = 0;
            decimal SendIntervalSeconds = 600;
            DateTime AppStartTimestamp = DateTime.Now;
            string connectionString = ConfigurationManager.ConnectionStrings["DataCenterEmailEngine"].ConnectionString;

            using (IDbConnection EmailDrop = new SqlConnection(connectionString))
            {
                CurrentEmailBatchID = EmailDrop.QuerySql<int>(
                    "EXEC WeeklyEmailBatches_GetNext @DropDate, @Realtime, @EmailServiceProvider_ID, @Processor_ID",
                    new { DropDate, Realtime, EmailServiceProvider_ID, Processor_ID = 1 }).Single();

                WeekCount = Convert.ToDecimal(EmailDrop.QuerySql<int>(
                    "EXEC WeeklyEmailBatchRecipients_GetCountV2 @EmailServiceProvider_ID, @Realtime",
                    new { EmailServiceProvider_ID, Realtime }).Single());


                List<RecipientModel> TheDrop = EmailDrop.QuerySql<RecipientModel>(
                    "EXEC WeeklyEmailBatchRecipients_GetV3 @EmailServiceProvider_ID, @EmailBatch_ID, @Realtime",
                    new { EmailServiceProvider_ID, EmailBatch_ID = CurrentEmailBatchID, Realtime }).ToList();

            }

        }
    }
}
