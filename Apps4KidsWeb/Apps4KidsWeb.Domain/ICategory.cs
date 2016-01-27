using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface ICategory
    /// </summary>
    public interface ICategory
    {
        /// <summary>
        /// The id
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The name
        /// </summary>
        string Name { get; }
    }
}
