﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private const string SQL_AddReservation = "insert into reservation values (@siteId, @name, @arrivalDate, @departureDate, @currentDateTime)";
        private string connectionString;
        
        public ReservationDAL(string connection)
        {
            connectionString = connection;
        }
        
        public int AddReservation( string CampSiteChoice, string name,  string ArrivalDate, string DepartureDate)
        {
            int reservationId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_AddReservation, connection);
                    cmd.Parameters.AddWithValue("@name", name );
                    cmd.Parameters.AddWithValue("@siteId", CampSiteChoice);
                    cmd.Parameters.AddWithValue("@ArrivalDate", ArrivalDate);
                    cmd.Parameters.AddWithValue("@departureDate", DepartureDate);
                    cmd.Parameters.AddWithValue("@currentDateTime", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("select max(reservation_id) as reservation_ID from reservation", connection);
                    SqlDataReader reader=cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        reservationId = Convert.ToInt32(reader["reservation_id"]);
                    }
                }
            }catch (SqlException)
            {
                throw;
            }
            return reservationId;
        }
    }
}
