using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace supertalentoftheworld.Manager
{
    public static class MailManager
    {
        private const string ID = "yyyoungh724@gmail.com";
        private const string PASSWORD = "dudgus724";


        public static void SendGmail(MailMessage msg)
        {

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ID, PASSWORD);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                client.Send(msg);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            }

            if (e.Error != null)
            {

            }
            else
            {

            }
        }
    }
}