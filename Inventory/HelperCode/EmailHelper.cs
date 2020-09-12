using Inventory.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Inventory.HelperCode
{
    public class EmailHelper
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = ConfigurationManager.AppSettings["senderEmail"].ToString(); //Sender Email Address  
        static string password = ConfigurationManager.AppSettings["senderEmailPass"].ToString(); //Sender Password  
        static string subject =  "تایید درخواست موتر سیار"; 
        static string body =  "Your request to mobile car has been approved."; 

        public static void SendEmail(string emailTo = "info.inventory.morr@gmail.com", 
            string _body = "Out side ministry")
        {
            var mobileCarTicketCtrl = new MobileCarTicketController();
            body = _body;
                //"<p>Your request approved to visit " + destination +"</p>"+"<p>Please contact to transport.</p>";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}