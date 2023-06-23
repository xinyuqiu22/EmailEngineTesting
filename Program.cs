using EASendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace EmailEngineTesting
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int EmailServiceProvider_ID = 7;
            DateTime Testtime = DateTime.Now;
            //PMTAservice.sendPMTA(EmailServiceProvider_ID, Testtime, false).GetAwaiter().GetResult();
            MailGunService.SendMailgunAsync(EmailServiceProvider_ID, Testtime).GetAwaiter().GetResult();
        }

        
    }
}