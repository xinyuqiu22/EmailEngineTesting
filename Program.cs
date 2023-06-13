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
            int EmailServiceProvider_ID = 0;
            PMTAservice.sendPMTA(EmailServiceProvider_ID, DateTime.Now, false);
        }

        
    }
}