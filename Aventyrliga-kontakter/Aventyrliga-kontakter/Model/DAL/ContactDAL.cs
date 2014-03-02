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
                            contact.EmailAddress = reader.GetString(emailIndex);

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
                    cmd.Parameters.Add("@ContactID",SqlDbType.Int, 4).Value = id;

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //hämtar index för alla kolumner.
                        int idIndex = reader.GetOrdinal("ContactID");
                        int fNameIndex = reader.GetOrdinal("FirstName");
                        int lNameIndex = reader.GetOrdinal("LastName");
                        int emailIndex = reader.GetOrdinal("EmailAddress");

                        if (reader.Read())
                        {
                            //kontakt som ska returneras
                            Contact retContact = new Contact();

                            //Kontakten fylls med data.
                            retContact.ContactId = reader.GetInt32(idIndex);
                            retContact.FirstName = reader.GetString(fNameIndex);
                            retContact.LastName = reader.GetString(lNameIndex);
                            retContact.EmailAddress = reader.GetString(emailIndex);

                            return retContact;
                        }

                    }
                    //Om ingen contact kunde skapas så returneras null.
                    return null;
                }

            }
            catch
            {
                throw new ApplicationException("Det gick inte att hämta tillbaka kontakten.");
            }

        }


        //lägg till ny kontakt i databasen
        public void InsertContact(Contact contact)
        {
            try
            {
                //öppnar anslutning
                using (SqlConnection conn = CreateConnection())
                {

                    //cmd innehåller info om den lagrade procedur som ska användas.
                    var cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //lägger till parametrar till kontakten som ska skapas
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    //output-parametern
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    //Den lagrade proceduren körs
                    cmd.ExecuteNonQuery();

                    //Output: Den nya kontaktens ID.
                    contact.ContactId = (int)cmd.Parameters["@ContactID"].Value;

                }

            }
            catch
            {
                throw new ApplicationException("Det gick inte att lägga till kontakten i databasen.");
            }

        }



        //Uppdatera kontakt i databasen //EJ KLAR
        public void UpdateContact(Contact contact)
        {
            try
            {
                //öppnar anslutning
                using (SqlConnection conn = CreateConnection())
                {

                    //cmd innehåller info om den lagrade procedur som ska användas.
                    var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //lägger till parametrar till kontakten som ska ändras
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    cmd.Parameters.Add("@RetValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;

                    conn.Open();

                    //Den lagrade proceduren körs
                    cmd.ExecuteNonQuery();

                    //Output: Returvärdet visar om det fungerade eller inte
                    int retValue = (int)cmd.Parameters["@RetValue"].Value;
                }

            }
            catch
            {
                throw new ApplicationException("Det gick inte att uppdatera kontakten i databasen.");
            }

        }

        //Ta bort kontakt från databasen //EJ KLAR
        public void DeleteContact(Contact contact)
        {
            try
            {
                //öppnar anslutning
                using (SqlConnection conn = CreateConnection())
                {

                    //cmd innehåller info om den lagrade procedur som ska användas.
                    var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //lägger till parameter: Kontakten som ska raderas.
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactId;

                    cmd.Parameters.Add("@RetValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;

                    conn.Open();

                    //Den lagrade proceduren körs
                    cmd.ExecuteNonQuery();

                    //Output: Returvärdet visar om det fungerade eller inte
                    int retValue = (int)cmd.Parameters["@RetValue"].Value;
                }

            }
            catch
            {
                throw new ApplicationException("Det gick inte att ta bort kontakten i databasen.");
            }

        }
       



    }   
}