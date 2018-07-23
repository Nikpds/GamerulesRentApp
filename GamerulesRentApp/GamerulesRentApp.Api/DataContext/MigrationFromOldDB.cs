using GamerulesRentApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.DataContext
{
    public class MigrationFromOldDB
    {
        private Context _db;

        public MigrationFromOldDB(Context db)
        {
            _db = db;
        }

        public async Task<string> ReadMDF()
        {
            List<Customer> customers = new List<Customer>();

            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RentalManager_DATA.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd;
            SqlDataReader dr;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM CUSTOMERS", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            while (dr.Read())
            {
                var cus = new Customer();
                cus.Name = dr[1].ToString();
                cus.Lastname = dr[2].ToString();
                cus.Mobile = dr[24].ToString();
                cus.Phone = dr[4].ToString();
                cus.IsVerified = true;
                cus.IdentityNo = dr[19].ToString();
                cus.Area = dr[6].ToString();
                cus.Created = DateTime.Now;
                if (dr[35] != null)
                {
                    cus.Address = dr[5].ToString() + " " + dr[35].ToString();
                }
                else
                {
                    cus.Address = dr[5].ToString();
                }

                if (cus.Name != "ΤΕΣΤ" && cus.Name != "ΤΕΣ")
                {
                    customers.Add(cus);
                }
            }

            var result = await _db.Customers.BulkInsert(customers);
            con.Close();
            return "ok";
        }
    }
}
