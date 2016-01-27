using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// Represents the implementation of IApp
    /// </summary>
    internal class App : IApp
    {
        #region properties

        /// <summary>
        /// The id
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The producer
        /// </summary>
        public string Producer { get; private set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The url
        /// </summary>
        public string URL { get; private set; }

        /// <summary>
        /// The ids of the images
        /// </summary>
        public IEnumerable<int> ImageIds { get; private set; }

        /// <summary>
        /// The recentions
        /// </summary>
        public IEnumerable<IRecention> Recentions { get; private set; }

        /// <summary>
        /// The operating systems
        /// </summary>
        public string AppOS { get; private set; }

        /// <summary>
        /// The price
        /// </summary>
        public double Price { get; private set; }

        /// <summary>
        /// The number of ratings
        /// </summary>
        public int Ratings { get; private set; }

        /// <summary>
        /// The average rating
        /// </summary>
        public double AverageRating
        {
            get
            {
                if (Ratings == 0)
                {
                    return 0;
                }
                return Math.Round(Recentions.Select(r => r.Rating).Average(), 2);
            }
        }

        /// <summary>
        /// The technical prerequisites
        /// </summary>
        public string Prerequisites { get; private set; }

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<ICategory> Categories { get; private set; }

        /// <summary>
        /// The ids of the operating systems
        /// </summary>
        public IEnumerable<int> OperatingSystems { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of App
        /// </summary>
        internal App()
        {

        }

        /// <summary>
        /// Initializes a new instance of App
        /// </summary>
        /// <param name="app">The database entity</param>
        internal App(Persistence.App app)
        {
            this.ID = app.ID;

            List<int> imageIds = new List<int>();
            foreach (var item in app.Pictures)
            {
                imageIds.Add(item.ID);
            }
            this.ImageIds = imageIds.ToArray();

            this.ImageIds = app.Pictures.Any() ? app.Pictures.Select(p => p.ID).ToArray() : new[] { 0 };

            this.Name = app.Designation;

            this.Producer = app.Producer.Designation;

            this.Description = app.Description;

            this.URL = app.URL;

            this.AppOS = string.Join(", ", app.OperatingSystems.Select(os => os.Designation));

            this.OperatingSystems = app.OperatingSystems.Select(os => os.ID).ToArray();

            this.Ratings = app.Recentions.Count;

            this.Price = Convert.ToDouble(app.Price);

            this.Prerequisites = app.Prerequisites;

            var categories = new List<ICategory>();
            foreach (var item in app.Categories)
            {
                categories.Add(new Category(item));
            }
            this.Categories = categories.ToArray();


            var recentions = new List<IRecention>();
            foreach (var item in app.Recentions)
            {
                recentions.Add(new Recention(item));
            }
            this.Recentions = recentions;
        }

        #endregion

        #region methods

        /// <summary>
        /// Removes an image from the app
        /// </summary>
        /// <param name="id">The id of the image</param>
        /// <returns>success</returns>
        public bool RemoveImage(int id)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.App app = context.Apps.Single(a=> a.ID == this.ID);
                    app.Pictures.Remove(app.Pictures.Single(p=> p.ID == id));
                    context.SaveChanges();                    
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes a category from the app
        /// </summary>
        /// <param name="id">The id of the category</param>
        /// <returns>success</returns>
        public bool RemoveCategory(int id)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.App app = context.Apps.Single(a => a.ID == this.ID);
                    app.Categories.Remove(app.Categories.Single(p => p.ID == id));
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes an operating system from the app
        /// </summary>
        /// <param name="id">The id of the operating system</param>
        /// <returns>success</returns>
        public bool RemoveOperatingSystem(int id)
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.App app = context.Apps.Single(a => a.ID == this.ID);
                    app.OperatingSystems.Remove(app.OperatingSystems.Single(p => p.ID == id));
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
    }
}
