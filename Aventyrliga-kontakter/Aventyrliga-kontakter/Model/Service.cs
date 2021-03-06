﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aventyrliga_kontakter.Model.DAL;


using System.ComponentModel.DataAnnotations;namespace Aventyrliga_kontakter.Model
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
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
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
            //Använder en extension method för att validera kontakten med data annotations
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults))
            {
                var ex = new ValidationException("Valideringsfel");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

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