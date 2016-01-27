using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Domain.Email;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The e-mail to be sent if a new user registers
    /// </summary>
    internal class ConfirmationMail : IEMail
    {
        #region properties

        /// <summary>
        /// The e-mail address
        /// </summary>
        public string EMailAddress { get; private set; }

        /// <summary>
        /// The subject
        /// </summary>
        public string Subject
        {
            get { return "Bestätigen Sie Ihren Apps4Kids Account"; }
        }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; private set; }
        
        #endregion

        #region constructer

        /// <summary>
        /// Initializes a new instance of ConfirmationMail
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="confirmationCode">The confirmation code</param>
        /// <param name="lastname">The lastname</param>
        /// <param name="firstname">The firstname</param>
        /// <param name="emailAddress">The e-mail address</param>
        public ConfirmationMail(int userId, string confirmationCode, string lastname, string firstname, string emailAddress)
        {
            this.EMailAddress = emailAddress;
            this.Message = GenerateMessage(userId, confirmationCode, lastname, firstname);
        }
        
        #endregion

        #region methods

        /// <summary>
        /// Generates the message
        /// </summary>
        /// <param name="userID">The id of the user</param>
        /// <param name="confirmationCode">The confirmation code</param>
        /// <param name="lastname">The lastname</param>
        /// <param name="firstname">The firstname</param>
        /// <returns></returns>
        private static string GenerateMessage(int userID, string confirmationCode, string lastname, string firstname)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Hallo {0} {1},", firstname, lastname);
            result.AppendLine();
            result.AppendLine("Bitte bestätigen Sie mittels Klick auf den Link:");
            result.AppendFormat("www.eltern.de/Apps4Kids/Login/ConfirmAccount?id={0}&confirmationCode={1}", userID, confirmationCode);
            result.AppendLine();
            result.AppendLine("die Authentizität Ihrer Registrierung.");
            result.AppendLine();
            result.AppendLine("Vielen Dank!");
            result.AppendLine();
            result.AppendLine("Ihre eltern.de Redaktion");


            return result.ToString();
        }
        
        #endregion
    }
}
