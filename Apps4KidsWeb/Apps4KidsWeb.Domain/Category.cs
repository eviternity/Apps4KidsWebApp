using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;

namespace Apps4KidsWeb.Domain
{
    internal class Category : ICategory
    {
        #region properties

        /// <summary>
        /// The id
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// the name
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of category
        /// </summary>
        internal Category()
        {

        }

        /// <summary>
        /// Initializes a new instance of category
        /// </summary>
        /// <param name="category">The database entity</param>
        internal Category(Persistence.Category category)
        {
            this.ID = category.ID;
            this.Name = category.Designation;
        }
       

        #endregion
    }
}
