using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;
using System.ComponentModel.DataAnnotations;

namespace Apps4KidsWeb.Models
{
    /// <summary>
    /// The implemention of IAppEx
    /// </summary>
    public class AddAppViewModel : IAppEx
    {
        #region fields

        private Dictionary<int, byte[]> images = new Dictionary<int, byte[]>();
        private Dictionary<int, int> operatingSystems = new Dictionary<int, int>();
        private Dictionary<int, int> categories = new Dictionary<int, int>();

        private Dictionary<int, int> picturesInDatabase = new Dictionary<int, int>();
        private Dictionary<int, int> categoriesInDatabase = new Dictionary<int, int>();
        private Dictionary<int, int> operatingSystemsInDatabase = new Dictionary<int, int>();

        #endregion

        #region properties        
        /// <summary>
        /// The app id
        /// </summary>
        [ScaffoldColumn(false)]
        public int? AppID { get; private set; }

        /// <summary>
        /// The guid
        /// </summary>
        [ScaffoldColumn(false)]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [ScaffoldColumn(false)]
        [DataType("Category")]
        public int Category { get; set; }

        /// <summary>
        /// Gets or sets the os.
        /// </summary>
        /// <value>
        /// The os.
        /// </value>
        [ScaffoldColumn(false)]
        [DataType("OperatingSystem")]
        [Range(1, int.MaxValue, ErrorMessage = "Sie müssen ein Betriebssystem eingeben um es hinzuzufügen")]
        public int OS { get; set; }

        /// <summary>
        /// The app name
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string AppName { get; set; }

        /// <summary>
        /// The producer
        /// </summary>
        [Display(Name="Hersteller")]
        [Required]
        [DataType("Producer")]
        [MinLength(1)]
        [MaxLength(50)]
        public string Producer { get; set; }

        /// <summary>
        /// The url
        /// </summary>
        [Required]
        [DataType(DataType.Url,ErrorMessage="Bitte geben Sie eine valide URL ein.")]
        [MaxLength(50)]
        public string URL { get; set; }

        /// <summary>
        /// The price
        /// </summary>
        [Display(Name = "Preis")]
        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        [Display(Name = "Beschreibung")]
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(5)]
        [MaxLength(4000)]
        public string Description { get; set; }

        /// <summary>
        /// The technical prerequisites
        /// </summary>
        [MinLength(1)]
        [MaxLength(50)]
        [Required]
        [Display(Name = "technische Vorbedingungen")]
        public string Prerequisites { get; set; }

        /// <summary>
        /// The ids of the operating systems
        /// </summary>
        [Display(Name = "Betriebssysteme")]
        public IDictionary<int, int> OperatingSystems { get { return operatingSystems; } }

        /// <summary>
        /// The images
        /// </summary>
        public IDictionary<int, byte[]> Images { get { return images; } }

        /// <summary>
        /// The categories
        /// </summary>
        public IDictionary<int, int> Categories { get { return categories; } }

        /// <summary>
        /// the categories to add (database entry allready exists)
        /// </summary>
        public IEnumerable<int> CategoriesToAdd
        {
            get { return categories.Where(c=> !categoriesInDatabase.Keys.Contains(c.Key)).Select(c=> c.Value); }
        }

        /// <summary>
        /// The images to add (database entry allready exists)
        /// </summary>
        public IEnumerable<byte[]> ImagesToAdd
        {
            get { return images.Where(i => !picturesInDatabase.Keys.Contains(i.Key)).Select(i => i.Value); }
        }

        /// <summary>
        /// The ids of the operatingsystems to add (database entry allready exists)
        /// </summary>
        public IEnumerable<int> OperatingSytemsToAdd
        {
            get { return operatingSystems.Where(os => !operatingSystemsInDatabase.Keys.Contains(os.Key)).Select(os => os.Value); }
        }

        #endregion

        #region events

        public event EventHandler Saved;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppViewModel"/> class.
        /// </summary>
        public AddAppViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppViewModel"/> class.
        /// </summary>
        /// <param name="app">The app.</param>
        public AddAppViewModel(IApp app)
        {
            this.AppName = app.Name;
            this.Description = app.Description;
            this.Prerequisites = app.Prerequisites;
            this.Price = app.Price;
            this.AppID = app.ID;
            this.URL = app.URL;
            this.Producer = app.Producer;

            foreach (int id in app.ImageIds)
            {
                byte[] data = Facade.GetPicture(id);

                int newId = this.AddImage(data);
                this.picturesInDatabase.Add(newId, id);
            }

            foreach (int categoryId in app.Categories.Select(c => c.ID))
            {
                int newId = this.AddCategory(categoryId);
                this.categoriesInDatabase.Add(newId, categoryId);
            }

            foreach (int osID in app.OperatingSystems)
            {
                int newId = this.AddOperatingSystem(osID);
                this.operatingSystemsInDatabase.Add(newId, osID);
            }
        }

        public AddAppViewModel(IRecommendationEx recommendation)
        {
            recommendation.Accept();
            this.AppName = recommendation.AppName;
            this.Description = recommendation.Description;
            this.AddOperatingSystem(recommendation.OperatingSystem);

        }

        #endregion

        #region methods

        private void OnSaved()
        {
            if (Saved != null)
            {
                Saved(this, new EventArgs());
            }
        }

        public int AddImage(byte[] image)
        {
            int newKey = images.Keys.Any() ? images.Keys.Max() + 1 : 1;
            images.Add(newKey, image);
            return newKey;
        }

        public void RemoveImage(int id)
        {
            if (Images.Keys.Contains(id))
            {
                images.Remove(id);
                if (picturesInDatabase.ContainsKey(id))
                {
                    IApp app = Facade.GetApp(this.AppID.Value);
                    app.RemoveImage(picturesInDatabase[id]);
                    picturesInDatabase.Remove(id);
                }
            }
        }

        public int AddOperatingSystem(int operatingSystem)
        {
            if (!operatingSystems.Values.Contains(operatingSystem))
            {
                int newKey = operatingSystems.Keys.Any() ? operatingSystems.Keys.Max() + 1 : 1;
                operatingSystems.Add(newKey, operatingSystem);
                return newKey;
            }
            return 0;
        }

        public void RemoveOperatingSystem(int id)
        {
            if (operatingSystems.Keys.Contains(id))
            {
                operatingSystems.Remove(id);
                if (operatingSystemsInDatabase.ContainsKey(id))
                {
                    IApp app = Facade.GetApp(this.AppID.Value);
                    app.RemoveOperatingSystem(operatingSystemsInDatabase[id]);
                    operatingSystemsInDatabase.Remove(id);
                }
            }
        }

        public int AddCategory(int category)
        {
            if (!categories.Values.Contains(category))
            {
                int newKey = categories.Keys.Any() ? categories.Keys.Max() + 1 : 1;
                categories.Add(newKey, category);
                return newKey;
            }
            return 0;
        }

        public void RemoveCategory(int id)
        {
            if (categories.Keys.Contains(id))
            {
                categories.Remove(id);
                if (categoriesInDatabase.ContainsKey(id))
                {
                    IApp app = Facade.GetApp(this.AppID.Value);
                    app.RemoveCategory(categoriesInDatabase[id]);
                    categoriesInDatabase.Remove(id);
                }
            }
        }

        public bool Save()
        {
            bool success = Facade.SaveApp(this);

            if (success)
            {
                OnSaved();
                return true;
            }
            
            return false;
        }

        #endregion


       
    }
}