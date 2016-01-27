using System;
using System.Collections.Generic;
namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IApp
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// The id
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The image ids
        /// </summary>
        IEnumerable<int> ImageIds { get; }

        /// <summary>
        /// The average rating
        /// </summary>
        double AverageRating { get; }

        /// <summary>
        /// The number of ratings
        /// </summary>
        int Ratings { get; }

        /// <summary>
        /// The name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The producer
        /// </summary>
        string Producer { get; }

        /// <summary>
        /// The operating systems
        /// </summary>
        string AppOS { get; }

        /// <summary>
        /// The price
        /// </summary>
        double Price { get; }

        /// <summary>
        /// The technical prerequisites
        /// </summary>
        string Prerequisites { get; }

        /// <summary>
        /// The description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The recentions
        /// </summary>
        IEnumerable<IRecention> Recentions { get; }

        /// <summary>
        /// The url
        /// </summary>
        string URL { get; }

        /// <summary>
        /// the categories
        /// </summary>
        IEnumerable<ICategory> Categories { get; }

        /// <summary>
        /// The ids of the operating systems
        /// </summary>
        IEnumerable<int> OperatingSystems { get; }

        /// <summary>
        /// Removes an image from the app
        /// </summary>
        /// <param name="id">The id of the image</param>
        /// <returns>success</returns>
        bool RemoveImage(int id);

        /// <summary>
        /// Removes a category from the app
        /// </summary>
        /// <param name="id">The id of the category</param>
        /// <returns>success</returns>
        bool RemoveCategory(int id);

        /// <summary>
        /// Removes an operating system from the app
        /// </summary>
        /// <param name="id">The id of the operating system</param>
        /// <returns>success</returns>
        bool RemoveOperatingSystem(int id);
    }
}
