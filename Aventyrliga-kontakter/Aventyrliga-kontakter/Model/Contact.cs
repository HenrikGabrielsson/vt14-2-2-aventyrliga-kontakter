using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aventyrliga_kontakter.Model
{
    public class Contact
    {
        //egenskaper för kolumner i Customertabellen
        public int ContactId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}