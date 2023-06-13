using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using Insight.Database;
using System.Web.Http;
using EASendMail;

namespace TestingORM
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
    class Program
    {
        static void Main(string[] args)
        {
            int EmailServiceProvider_ID = 0;
            InsightDatabaseTest(EmailServiceProvider_ID);
        }

        public static void sendPMTA(int EmailServiceProvider_ID, DateTime DropDate, bool Realtime)
        {

        }

        public static void InsightDatabaseTest(int EmailServiceProvider_ID)
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["DataCenterEmailEngine"].ConnectionString;

            // Create a new Microsoft.Data.SqlClient.SqlConnection using the connection string
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                // Execute the stored procedure TEST_EmailBatchRecipients_Get using Insight.Database's Query method
                // Pass the EmailServiceProvider_ID as a parameter and specify the command type as StoredProcedure
                // Map the results to a List of EngineSettings objects
                IEnumerable<EngineSettings> ES = connection.QuerySql<EngineSettings>("EXEC TEST_EmailBatchRecipients_Get @EmailServiceProvider_ID", new { EmailServiceProvider_ID }).ToList();
                Console.WriteLine(ES.Count());
            }
        }

       

    }
}