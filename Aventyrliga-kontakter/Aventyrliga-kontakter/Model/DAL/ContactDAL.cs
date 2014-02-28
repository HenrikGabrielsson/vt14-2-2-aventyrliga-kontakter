using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Aventyrliga_kontakter.Model.DAL
{
    public class ContactDAL : DALBase
    {
        //hämta kontakter från databasen
        public IEnumerable<Contact> GetContacts()
        {
            try
            {
                //listan ska fyllas med kontakterna i databasen.
                List<Contact> contacts = new List<Contact>(100);

                //ansluter...
                using (SqlConnection conn = CreateConnection())
                {

                    //Den lagrade proceduren uspGetContact ska användas för att hämta ut kontakterna
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    //Raderna hämtas en efter en
                    using (var reader = cmd.ExecuteReader())
                    {
                        //hämtar index för alla kolumner.
                        int idIndex = reader.GetOrdinal("ContactID");
                        int fNameIndex = reader.GetOrdinal("FirstName");
                        int lNameIndex = reader.GetOrdinal("LastName");
                        int emailIndex = reader.GetOrdinal("EmailAddress");


                        while (reader.Read())
                        {
                            //skapar ny kontakt och hämtar all information från tabellen.
                            Contact contact = new Contact();
                            contact.ContactId = reader.GetInt32(idIndex);
                            contact.FirstName = reader.GetString(fNameIndex);
                            contact.LastName = reader.GetString(lNameIndex);
                            contact.EmailAdress = reader.GetString(emailIndex);

                            //lägger till kontakterna i listan
                            contacts.Add(contact);
                        }
                    }


                }


                //plocka bort de tomma posterna i listan och returnerar
                contacts.TrimExcess();
                return contacts;
            }

            catch
            {
                throw new ApplicationException("Det gick inte att hämta kontaktlistan!");
            }
        }


        //hämta enskild kontakt med givet ID
        public Contact GetContactById(int id)
        {
            try
            {

                //öppnar anslutning
                using(SqlConnection conn = CreateConnection())
                {
                    //cmd innehåller info om den lagrade procedur som ska användas.
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //lägger till en parameter: ContactID
                    cmd.Parameters.AddWithValue("@ContactID", id);

                    conn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        //hämtar index för alla kolumner.
                        int idIndex = reader.GetOrdinal("ContactID");
                        int fNameIndex = reader.GetOrdinal("FirstName");
                        int lNameIndex = reader.GetOrdinal("LastName");
                        int emailIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            //kontakt som ska returneras
                            Contact retContact = new Contact();

                            //Kontakten fylls med data.
                            retContact.ContactId = reader.GetInt32(idIndex);
                            retContact.FirstName = reader.GetString(fNameIndex);
                            retContact.LastName = reader.GetString(lNameIndex);
                            retContact.EmailAdress = reader.GetString(emailIndex);

                            return retContact;
                        }
                    }

                }
                //Om ingen contact kunde skapas så returneras null.
                return null;
            }
            catch
            {
                throw new ApplicationException("Det gick inte att hämta tillbaka kontakten.");
            }

        }
    }   
}