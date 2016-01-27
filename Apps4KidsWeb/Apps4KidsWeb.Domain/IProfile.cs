namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IProfile
    /// </summary>
    public interface IProfile
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The firstname
        /// </summary>
        string Firstname { get; }

        /// <summary>
        /// The lastname
        /// </summary>
        string Lastname { get; }

        /// <summary>
        /// The children
        /// </summary>
        string Children { get; }

        /// <summary>
        /// The id of the country of origin
        /// </summary>
        int CountryOfOriginID { get; }
    }
}
