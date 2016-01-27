using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;
using Apps4KidsWeb.Domain.Email;

namespace Apps4KidsWeb.Domain
{
    public static class Facade
    {
        private static Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        #region UserControl

        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <param name="login">The login data</param>
        /// <returns>The user</returns>
        public static IUser Login(ILogin login)
        {
            using (var context = Connection.GetContext())
            {
                sp_Login_Result result = context.sp_Login(login.UserName, login.Password).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return new User(result);
            }
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registration">The registration data</param>
        /// <returns>success</returns>
        public static bool CreateUser(IRegistration registration)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    string confirmationCode = GenerateAuthenticationCode();

                    context.sp_RegisterUser(
                        registration.Username,
                        registration.Password,
                        registration.Firstname,
                        registration.Lastname,
                        registration.Children,
                        registration.CountryOfOriginId,
                        confirmationCode);
                    
                    context.SaveChanges();
                    
                    Persistence.User newUser = context.Users.SingleOrDefault(u => u.EMail == registration.Username && u.AuthentificationCode != null);
                    
                    if (newUser == null)
                    {
                        return false;
                    }

                    var mail = new ConfirmationMail(newUser.ID, confirmationCode, registration.Lastname, registration.Firstname, registration.Username);
                    
                    Email.EmailSender.SendEmail(mail);

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Authentificates a user with the code from the e-mail
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="code">The authentification code</param>
        /// <returns>The user</returns>
        public static IUser AuthentificateUser(int id, string code)
        {
            using (var context = Connection.GetContext())
            {
                sp_Login_Result result = context.sp_Authentificate(id, code).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return new User(result);
            }
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user</returns>
        public static IUser GetUser(string id)
        {
            using (var context = Connection.GetContext())
            {
                int userId = int.Parse(id);
                Persistence.User user = context.Users.Include("Admin").FirstOrDefault(u => u.ID == userId);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return new User(user);
                }
            }
        }

        /// <summary>
        /// Sends an e-mail to the user containing the link to change his password
        /// </summary>
        /// <param name="email">The email address</param>
        /// <returns>success</returns>
        public static bool SendPasswordForgottenMail(string email)
        {
            using (var context = Connection.GetContext())
            {
                var user = context.Users.FirstOrDefault(u => u.EMail == email);

                if (user == null)
                {
                    return false;
                }

                string confirmationCode = GenerateAuthenticationCode();

                context.ChangePasswordCodes.RemoveRange(context.ChangePasswordCodes.Where(cpc => cpc.UserID == user.ID));
                context.SaveChanges();

                context.ChangePasswordCodes.Add(new ChangePasswordCode() { UserID = user.ID, Code = confirmationCode });
                context.SaveChanges();


                var mail = new ChangePasswordMail(user.ID, confirmationCode, user.LastName, user.FirstName, email);
                EmailSender.SendEmail(mail);

                return true;
            }
        }

        /// <summary>
        /// Changes the password of a user with the code of the confirmation mail
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="password">The new password</param>
        /// <param name="code">The confirmation code</param>
        /// <returns>The user</returns>
        public static IUser ChangePassword(int id, string password, string code)
        {
            using (var context = Connection.GetContext())
            {
                sp_Login_Result result = context.sp_changePassword(id, password, code).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return new User(result);
            }
        }

        /// <summary>
        /// Alters the profile of a user
        /// </summary>
        /// <param name="profile">The profile data</param>
        /// <returns>success</returns>
        public static bool AlterProfile(IProfile profile)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.User user = context.Users.SingleOrDefault(u => u.ID == profile.ID);
                    if (user == null)
                    {
                        return false;
                    }
                    user.FirstName = profile.Firstname;
                    user.LastName = profile.Lastname;
                    user.Children = profile.Children;
                    user.CountryOfOriginID = profile.CountryOfOriginID;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        #region AppControl

        /// <summary>
        /// Gets an app
        /// </summary>
        /// <param name="id">The id of the app</param>
        /// <returns>The app</returns>
        public static IApp GetApp(int id)
        {
            using (var context = Connection.GetContext())
            {
                Persistence.App app =
                    context.Apps
                    .Include("Pictures")
                    .Include("Categories")
                    .Include("OperatingSystems")
                    .Include("Recentions")
                    .Include("Recentions.User")
                    .Include("Producer")
                    .SingleOrDefault(a => a.ID == id);
                return new App(app);
            }
        }

        /// <summary>
        /// Gets a searchresult of all apps
        /// </summary>
        /// <returns>The searchresult</returns>
        public static ISearchResult SearchApps()
        {
            return new SearchResult();
        }

        /// <summary>
        /// Gets a searchresult with given criterea
        /// </summary>
        /// <param name="criteria">The criteria</param>
        /// <returns>The searchresult</returns>
        public static ISearchResult SearchApps(ISearchCriteria criteria)
        {
            return new SearchResult(criteria);
        }

        /// <summary>
        /// Gets a searchresult with given critera and page
        /// </summary>
        /// <param name="criteria">The criteria</param>
        /// <param name="page">The page</param>
        /// <returns>The searchresult</returns>
        public static ISearchResult SearchApps(ISearchCriteria criteria, int page)
        {
            return new SearchResult(criteria, page);
        }

        /// <summary>
        /// Saves new apps and changes in existing apps
        /// </summary>
        /// <param name="appex">The app data</param>
        /// <returns>success</returns>
        public static bool SaveApp(IAppEx appex)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.App app;

                    if (appex.AppID.HasValue)
                    {
                        app = context.Apps.Single(a => a.ID == appex.AppID.Value);
                        app.Designation = appex.AppName;
                        app.Description = appex.Description;
                        app.Price = Convert.ToDecimal(appex.Price);
                        app.ProducerID = ProducerID.Get(appex.Producer);
                        foreach (var item in appex.ImagesToAdd)
                        {
                            app.Pictures.Add(new Picture() { Data = item });
                        }
                        foreach (var item in appex.CategoriesToAdd)
                        {
                            app.Categories.Add(context.Categories.Single(c => c.ID == item));
                        }
                        foreach (var item in appex.OperatingSytemsToAdd)
                        {
                            app.OperatingSystems.Add(context.AppOS.Single(c => c.ID == item));
                        }

                    }
                    else
                    {
                        app = new Persistence.App()
                                            {
                                                Designation = appex.AppName,
                                                Description = appex.Description,
                                                Prerequisites = appex.Prerequisites,
                                                Price = Convert.ToDecimal(appex.Price), 
                                                ProducerID = ProducerID.Get(appex.Producer),
                                                URL = appex.URL
                                            };

                        foreach (var item in appex.Images.Values)
                        {
                            app.Pictures.Add(new Picture() { Data = item });
                        }

                        foreach (var item in appex.OperatingSytemsToAdd)
                        {
                            app.OperatingSystems.Add(context.AppOS.Single(os => os.ID == item));
                        }

                        foreach (var item in appex.CategoriesToAdd)
                        {
                            app.Categories.Add(context.Categories.Single(c => c.ID == item));
                        }
                        context.Apps.Add(app);
                    }


                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        #endregion

        #region RecommendationControl

        /// <summary>
        /// Gets all recommendations
        /// </summary>
        /// <returns>The recommendations</returns>
        public static IEnumerable<IRecommendationEx> GetRecommendations()
        {
            using (var context = Connection.GetContext())
            {
                var query = context.Recommendations.Where(r => r.Approved == null && r.Denied == null);
                List<IRecommendationEx> result = new List<IRecommendationEx>();
                foreach (var item in query)
                {
                    result.Add(new Recommendation(item));
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// Gets a recommendation
        /// </summary>
        /// <param name="id">The id of the recommendation</param>
        /// <returns>The recommendation</returns>
        public static IRecommendationEx GetRecommendation(int id)
        {
            using (var context = Connection.GetContext())
            {
                Persistence.Recommendation recommendation = context.Recommendations.SingleOrDefault(r => r.ID == id);
                if (recommendation == null)
                {
                    return null;
                }
                return new Recommendation(recommendation);
            }
        }


        #endregion

        #region DropdownListItems

        /// <summary>
        /// Gets the countries of origin
        /// </summary>
        public static IDictionary<int, string> CountriesOfOrigin
        {
            get
            {
                using (var context = Connection.GetContext())
                {
                    return context.CountryOfOrigins.ToDictionary(c => c.ID, c => c.Designation);
                }
            }
        }

        /// <summary>
        /// Gets the app categories
        /// </summary>
        public static IDictionary<int, string> AppCategories
        {
            get
            {
                using (var context = Connection.GetContext())
                {
                    return context.Categories.ToDictionary(ac => ac.ID, ac => ac.Designation);
                }
            }
        }

        /// <summary>
        /// Gets the operating systems
        /// </summary>
        public static IDictionary<int, string> OperatingSystems
        {
            get
            {
                using (var context = Connection.GetContext())
                {
                    return context.AppOS.ToDictionary(os => os.ID, os => os.Designation);
                }
            }
        }

        /// <summary>
        /// Gets the produces
        /// </summary>
        public static IDictionary<int, string> Producers
        {
            get 
            {
                using (var context = Connection.GetContext())
                {
                    return context.Producers.ToDictionary(p=> p.ID, p=> p.Designation);
                }
            }
        }

        #endregion

        #region Pictures

        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="id">The id of the picture</param>
        /// <returns>The picture</returns>
        public static byte[] GetPicture(int id)
        {
            using (var context = Persistence.Connection.GetContext())
            {
                Persistence.Picture picture = context.Pictures.SingleOrDefault(p => p.ID == id);
                if (picture == null)
                {
                    return null;
                }
                return picture.Data;
            }
        }

        #endregion

        #region HelperMethods

        /// <summary>
        /// Generates a random 30 characters long alphanumeric code (Numbers and Uppercase Letters)
        /// </summary>
        /// <returns></returns>
        private static string GenerateAuthenticationCode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                if (rnd.Next(0, 2) == 0)
                {
                    result.Append(Convert.ToChar(rnd.Next(48, 58)));   // Numbers
                }
                else
                {
                    result.Append(Convert.ToChar(rnd.Next(65, 91)));   // Uppercase Letters
                }
            }
            return result.ToString();
        }

        #endregion
    }
}
