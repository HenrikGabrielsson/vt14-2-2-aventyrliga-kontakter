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
            startRowIndex += maximumRows;
            startRowIndex = startRowIndex / maximumRows;
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }


        //Vid skapande av ny post
        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.SaveContact(contact);

                    //Visa rättmeddelande
                    SuccessLabel.Text = String.Format("{0} {1} har lagts till!", contact.FirstName, contact.LastName);
                    SuccessPanel.Visible = true;

                }
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
                if (ModelState.IsValid)
                {

                    //kollar så kunden finns innan den sparas
                    var contact = Service.GetContact(contactID);
                    if (contact == null)
                    {
                        ModelState.AddModelError("", String.Format("Det gick inte att uppdatera kontakten."));
                        return;
                    }
                    
                    if (TryUpdateModel(contact))
                    {
                        Service.SaveContact(contact);

                        //Visa rättmeddelande
                        SuccessLabel.Text = String.Format("{0} {1} har uppdaterats!", contact.FirstName, contact.LastName);
                        SuccessPanel.Visible = true;

                    }
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

                SuccessLabel.Text = "Kontakten har tagits bort";
                SuccessPanel.Visible = true;

            }
            catch
            {
                ModelState.AddModelError(String.Empty, String.Format("Det gick inte att ta bort kontakten."));
            }
        }


    }
}