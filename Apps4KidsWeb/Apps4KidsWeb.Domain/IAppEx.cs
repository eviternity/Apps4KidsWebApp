using System;
using System.Collections.Generic;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IAppEx
    /// </summary>
    public interface IAppEx
    {
        /// <summary>
        /// The app id
        /// </summary>
        int? AppID { get; }

        /// <summary>
        /// The app name
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// The producer
        /// </summary>
        string Producer { get; }

        /// <summary>
        /// The categories
        /// </summary>
        IDictionary<int, int> Categories { get; }

        /// <summary>
        /// the categories to add (database entry allready exists)
        /// </summary>
        IEnumerable<int> CategoriesToAdd { get; }

        /// <summary>
        /// The description
        /// </summary>
        string Description { get;  }

        /// <summary>
        /// The guid
        /// </summary>
        string Guid { get;  }

        /// <summary>
        /// The technical prerequisites
        /// </summary>
        string Prerequisites { get; }

        /// <summary>
        /// The url
        /// </summary>
        string URL { get; }

        /// <summary>
        /// The price
        /// </summary>
        double Price { get; }

        /// <summary>
        /// The images
        /// </summary>
        IDictionary<int, byte[]> Images { get; }

        /// <summary>
        /// The images to add (database entry allready exists)
        /// </summary>
        IEnumerable<byte[]> ImagesToAdd { get; }

        /// <summary>
        /// The ids of the operating systems
        /// </summary>
        IDictionary<int, int> OperatingSystems { get; }

        /// <summary>
        /// The ids of the operatingsystems to add (database entry allready exists)
        /// </summary>
        IEnumerable<int> OperatingSytemsToAdd { get; }
    }
}
