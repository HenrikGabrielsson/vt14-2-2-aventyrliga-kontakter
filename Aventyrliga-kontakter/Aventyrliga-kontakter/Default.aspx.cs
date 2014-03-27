using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aventyrliga_kontakter.Model;

namespace Aventyrliga_kontakter
{
    public partial class Default : System.Web.UI.Page
    {

        //Ett service-objekt för att anropa dess metoder
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }



        //Fyller tabellen med data
        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }


        //Vid skapande av ny post
        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                Service.SaveContact(contact);
            }
            catch
            {
                ModelState.AddModelError(String.Empty, String.Format("Det gick inte att lägga till kontakten."));
            }
        }


        //Vid uppdatering av en post
        public void ContactListView_UpdateItem(int contactID)
        {
            try
            {
                //kollar så kunden finns innan den sparas
                var item = Service.GetContact(contactID);
                if (item == null)
                {
                    ModelState.AddModelError("", String.Format("Det gick inte att uppdatera kontakten."));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    Service.SaveContact(item);

                }
            }
            catch
            {
                ModelState.AddModelError(String.Empty, String.Format("Det gick inte att uppdatera kontakten."));
            }
                
        }

        //Vid borttagning av en post
        public void ContactListView_DeleteItem(int contactID)
        {
            try
            {
                Service.DeleteContact(contactID);
            }
            catch
            {
                ModelState.AddModelError(String.Empty, String.Format("Det gick inte att ta bort kontakten."));
            }
        }


    }
}