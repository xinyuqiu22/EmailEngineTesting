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
            //0   All Others
            //1   Microsoft
            //2   Verizon
            //3   AT & T
            //4   Roadrunner
            //5   Comcast
            //7   GMail
            //8   Cox Cable
            //9   Ameritrade
            int Service = -1;
            int EmailServiceProvider_ID = -1;
            DateTime DropDate = DateTime.Now;
            if (args.Length > 0) int.TryParse(args[0], out Service);
            if (args.Length > 1) int.TryParse(args[1], out EmailServiceProvider_ID);
            if (args.Length > 2) DateTime.TryParse(args[2], out DropDate);
            switch (Service)
            {
                case 1:
                    PMTAservice.sendPMTA(EmailServiceProvider_ID, DropDate, false).GetAwaiter().GetResult();
                    break;
                case 2:
                    MailGunService.SendMailgunAsync(EmailServiceProvider_ID, DropDate).GetAwaiter().GetResult();
                    break;
                //case 3:
                //    SendGridService.sendSendGrid(EmailServiceProvider_ID, DropDate);
                //    break;
                case 4:
                    PMTAservice.sendPMTA(EmailServiceProvider_ID, DropDate, true).GetAwaiter().GetResult();
                    break;
                //case 5:
                //    SendInBlueService.sendSendInBlue(EmailServiceProvider_ID, DropDate);
                //    break;
                default:
                    // code block
                    break;
            }
            //int EmailServiceProvider_ID = 7;
            //DateTime Testtime = DateTime.Now;
            //PMTAservice.sendPMTA(EmailServiceProvider_ID, Testtime, false).GetAwaiter().GetResult();
            //MailGunService.SendMailgunAsync(EmailServiceProvider_ID, Testtime).GetAwaiter().GetResult();
        }

        
    }
}