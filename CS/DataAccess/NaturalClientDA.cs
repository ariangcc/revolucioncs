﻿using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataAccess {
    public class NaturalClientDA {
        public int searchNaturalClient(string dni) {
            int idNaturalClient = -1;
            MySqlConnection con = new MySqlConnection(Constants.connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select Person_IdPerson from NaturalClient where Dni = " + dni;

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                idNaturalClient = reader.GetInt32("Person_IdPerson");
            }
            con.Close();
            return idNaturalClient;
        }
        public BindingList<Person> listPeople()
        {
            BindingList<Person> list = new BindingList<Person>();
            MySqlConnection con = new MySqlConnection(Constants.connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM LegalClient";
            MySqlDataReader reader = cmd.ExecuteReader();
            Person p = new Person();
            while (reader.Read())
            {
                //para persona natural
                list.Add(p);
            }
            cmd.CommandText = "SELECT * FROM LegalClient";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //para persona jurídica
                list.Add(p);
            }
            con.Close();
            return list;
        }

        public void addNaturalClient(NaturalClient nc)
        {
            MySqlConnection con = new MySqlConnection(Constants.connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "addToBDNaturalClient";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("_address", MySqlDbType.String).Value = nc.Address;
            cmd.Parameters.Add("_phoneNumber", MySqlDbType.String).Value = nc.PhoneNumber;
            cmd.Parameters.Add("_email", MySqlDbType.String).Value = nc.Email;
            cmd.Parameters.Add("_name", MySqlDbType.String).Value = nc.Name;
            cmd.Parameters.Add("_surname", MySqlDbType.String).Value = nc.Surname;
            cmd.Parameters.Add("_dni", MySqlDbType.String).Value = nc.Dni;

            cmd.ExecuteNonQuery();
        }
    }
}
