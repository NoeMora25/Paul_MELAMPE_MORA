using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Client : ICrud<Client>
    {
        private int client_id;
        private string nom;
        private string prenom;
        private string telephone;
        private string mail;

        public Client()
        {
        }

        public Client(string nom, string prenom, string telephone, string mail)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Telephone = telephone;
            this.Mail = mail;
        }

        public Client(int id,string nom, string prenom, string telephone, string mail)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Telephone = telephone;
            this.Mail = mail;
        }

        public int Id
        {
            this.Client_id = client_id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Telephone = telephone;
            this.Mail = mail;
        }

            set
            {
                this.client_id = value;
            }


        }

        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }

        public string Prenom
        {
            get { return this.prenom; }
            set { this.prenom = value; }
        }

        public string Telephone
        {
            get { return this.telephone; }
            set { this.telephone = value; }
        }

        public string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }

        public int Create() { throw new NotImplementedException(); }
        public int Delete() { throw new NotImplementedException(); }

        public List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            string sql = "select * from client c;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Client leClient = new Client(
                        (int)dr["client_id"],
                        (string)dr["nom"],
                        dr["prenom"].ToString(),
                        (string)dr["telephone"],
                        dr["mail"].ToString()
                    );
                    lesClients.Add(leClient);
                }
            }
            return lesClients;
        }

        public List<Client> FindBySelection(string criteres) { throw new NotImplementedException(); }
        public void Read() { throw new NotImplementedException(); }
        public int Update() { throw new NotImplementedException(); }
    }
}