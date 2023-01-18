using System;
using System.Data;
using ContactBookRest.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NLog;


namespace ContactBookRest.Repository
{
    public class BaseRepository
    {
        public List<Contact> GetAllContact()
        {
            List<Contact> contacts = new List<Contact>();

            SqlCommand command = new SqlCommand
            {
                CommandText = @"usp_GetContacts",
                CommandType = CommandType.StoredProcedure
            };
            //command.Parameters.Add(new SqlParameter("@Name",SqlDbType.NVarChar)).Value = 

            DataSet data = DBAccess.GetDatasetSql(command);

            if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    Contact contact = new Contact();
                    contact.Name = row["Name"].ToString();
                    contact.Surname = row["Surname"].ToString();
                    contact.Numbers = row["PhoneNumbers"].ToString();

                    contacts.Add(contact);

                }
            }
            else if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count == 0)
            {
                return contacts;
            }
            else
            {
                //throw new SqlException();
                return null;
            }

            return contacts;
        }

        public string GetTest()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = @"SELECT * FROM [dbo].[Contacts]",
                CommandType = CommandType.Text
            };

            DataSet data = DBAccess.GetDatasetSql(command);

            if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                return "ok";
            }

            return "nie ok";
        }
    }
}
