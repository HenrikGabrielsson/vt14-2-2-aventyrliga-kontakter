using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aventyrliga_kontakter.Model.DAL;

namespace Aventyrliga_kontakter.Model
{
    public class Service
    {
        //Dataåtkomstlagret
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }



        //hämta alla kontakter
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();    
        }

        //hämta en kontakt
        public Contact GetContact(Contact contact)
        {
            return ContactDAL.GetContactById(contact.ContactId);
        }



        //Funktioner som tar bort kontakter 
        //Argument: En kontakt
        public void DeleteContact(Contact contact)
        {
            ContactDAL.DeleteContact(contact);
        }

        //Argument: Ett ID
        public void DeleteContact(int contactID)
        {
            Contact contact = new Contact();
            contact.ContactId = contactID;

            ContactDAL.DeleteContact(contact);
        }



        //Funktion som skapar eller uppdaterar en befintlig kontakt
        public void SaveContact(Contact contact)
        {

            //Om det är en ny kontakt
            if(contact.ContactId == 0)
            {
                ContactDAL.InsertContact(contact);
            }

            //Om det är en befintlig kontakt
            else
            {
                ContactDAL.UpdateContact(contact);
            }

        }


    }
}