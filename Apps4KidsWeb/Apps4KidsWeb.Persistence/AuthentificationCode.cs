//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps4KidsWeb.Persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthentificationCode
    {
        public int UserID { get; set; }
        public string Code { get; set; }
    
        public virtual User User { get; set; }
    }
}
