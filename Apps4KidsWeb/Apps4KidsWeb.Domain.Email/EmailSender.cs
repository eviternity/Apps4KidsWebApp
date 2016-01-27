using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain.Email
{
    public static class EmailSender
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="mail">The e-mail.</param>
        public static void SendEmail(IEMail mail)
        {
            //todo send real e-mail
            Debug.WriteLine("Email wird versendet.");
            Debug.WriteLine(string.Format("Addresse:\t {0}",mail.EMailAddress));
            Debug.WriteLine(string.Format("Betreff:\t {0}", mail.Subject));
            Debug.WriteLine(string.Format("Inhalt:\n {0}", mail.Message));
        }
    }
}
