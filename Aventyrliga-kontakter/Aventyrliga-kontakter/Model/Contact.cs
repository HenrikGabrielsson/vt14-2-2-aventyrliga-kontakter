using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aventyrliga_kontakter.Model
{
    public class Contact
    {
        //egenskaper för kolumner i Customertabellen
        public int ContactId { get; set; }

        [Required(ErrorMessage="Du måste fylla i en Email-adress!")]
        [StringLength(50,ErrorMessage="Email-adressen får inte vara längre än 50 tecken!")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email-adressen har inte ett giltigt format!")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett förnamn!")]
        [StringLength(50, ErrorMessage = "Förnamnet får inte vara längre än 50 tecken!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett efternamn!")]
        [StringLength(50, ErrorMessage = "Efternamnet får inte vara längre än 50 tecken!")]
        public string LastName { get; set; }
    }
}