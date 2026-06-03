using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Commande : ICrud<Commande>
    {
        private int commande_id;
        private DateOnly date_creation;
        private DateOnly date_retrait;
        private decimal acompte;
        private bool est_prete;
        private bool est_recuperee;
        private decimal total;
        private DateOnly date_evenement;
        private int nb_personne;
        private Client client;
        private Categorie_evenement categorie_evenement;

        public Commande() { }


        public Commande()
        {
        }

        public Commande(DateOnly date_creation, DateOnly date_retrait, decimal acompte, bool est_prete, bool est_recuperee, decimal total, DateOnly date_evenement, int nb_personne, Client client, Categorie_evenement categorie_evenement)
        {
            this.Date_creation = date_creation;
            this.Date_retrait = date_retrait;
            this.Acompte = acompte;
            this.Est_prete = est_prete;
            this.Est_recuperee = est_recuperee;
            this.Total = total;
            this.Date_evenement = date_evenement;
            this.Nb_personne = nb_personne;
            this.Client = client;
            this.Categorie_evenement = categorie_evenement;
        }

        public Commande(int id, DateOnly date_creation, DateOnly date_retrait, decimal acompte, bool est_prete, bool est_recuperee, decimal total, DateOnly date_evenement, int nb_personne, Client client, Categorie_evenement categorie_evenement)
        {
            this.Id = id;
            this.Date_creation = date_creation;
            this.Date_retrait = date_retrait;
            this.Acompte = acompte;
            this.Est_prete = est_prete;
            this.Est_recuperee = est_recuperee;
            this.Total = total;
            this.Date_evenement = date_evenement;
            this.Nb_personne = nb_personne;
            this.Client = client;
            this.Categorie_evenement = categorie_evenement;
        }

        public int Commande_id
        {
            get { return this.commande_id; }
            set { this.commande_id = value; }
        }

            set
            {
                this.commande_id = value;
            }

        }

        public DateOnly Date_creation
        {
            get
            {
                return this.date_creation;
            }

            set
            {
                this.date_creation = value;
            }
        }

        public DateOnly Date_retrait
        {
            get
            {
                return this.date_retrait;
            }

            set
            {
                this.date_retrait = value;
            }
        }

        public decimal Acompte
        {
            get
            {
                return this.acompte;
            }

            set
            {
                this.acompte = value;
            }
        }

        public bool Est_prete
        {
            get
            {
                return this.est_prete;
            }

            set
            {
                this.est_prete = value;
            }
        }

        public bool Est_recuperee
        {
            get
            {
                return this.est_recuperee;
            }

            set
            {
                this.est_recuperee = value;
            }
        }

        public decimal Total
        {
            get
            {
                return this.total;
            }

            set
            {
                this.total = value;
            }
        }

        public DateOnly Date_evenement
        {
            get
            {
                return this.date_evenement;
            }

            set
            {
                this.date_evenement = value;
            }
        }

        public int Nb_personne
        {
            get
            {
                return this.nb_personne;
            }

            set
            {
                this.nb_personne = value;
            }
        }

        public Client Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        public Categorie_evenement Categorie_evenement
        {
            get
            {
                return this.categorie_evenement;
            }

            set
            {
                this.categorie_evenement = value;
            }
        }

        public void CalculerAcompte()
        {

        }
        public void CalculerTotal()
        {

        }

        public int Create()
        {
            string sql = @"INSERT INTO commande 
                           (date_creation, date_retrait, acompte, est_prete, est_recuperee, total, nb_personne, client_id) 
                           VALUES 
                           (@date_creation, @date_retrait, @acompte, @est_prete, @est_recuperee, @total, @nb_personne, @client_id) 
                           RETURNING commande_id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@date_creation", this.Date_creation);
                cmd.Parameters.AddWithValue("@date_retrait", this.Date_retrait);
                cmd.Parameters.AddWithValue("@acompte", this.Acompte);
                cmd.Parameters.AddWithValue("@est_prete", this.Est_prete);
                cmd.Parameters.AddWithValue("@est_recuperee", this.Est_recuperee);
                cmd.Parameters.AddWithValue("@total", this.Total);
                cmd.Parameters.AddWithValue("@nb_personne", this.Nb_personne);

                cmd.Parameters.AddWithValue("@client_id", this.Client.Client_id);

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();

            string sql = @"
            select *,*,*
            from commande c
            left join client cl ON c.client_id = cl.client_id
            left join categorie_evenement e on c.categorie_evenement_id = e.categorie_evenement_id;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Categorie_evenement uneCategorieEvenement = new Categorie_evenement(
                        (int)dr["categorie_evenement_id"],
                        dr["categorie_evenement_nom"].ToString()
                    );

                    Client unClient = new Client(
                        (int)dr["client_id"],
                        (string)dr["nom"],
                        dr["prenom"].ToString(),
                        dr["telephone"].ToString(),
                        dr["mail"].ToString()

                    );

                    Commande laCommande = new Commande(
                        (int)dr["commande_id"],
                        (DateOnly)dr["date_creation"],
                        (DateOnly)dr["date_retrait"],
                        (decimal)dr["acompte"],
                        (bool)dr["est_prete"],
                        (bool)dr["est_recuperee"],
                        (decimal)dr["total"],
                        (DateOnly)dr["date_evenement"],
                        (int)dr["nb_personne"],
                        unClient,
                        uneCategorieEvenement

                    );

                    lesCommandes.Add(laCommande);
                }
            }
            return lesCommandes;
        }

        public void Read() { throw new NotImplementedException(); }
        public int Update() { throw new NotImplementedException(); }
        public int Delete() { throw new NotImplementedException(); }
        public List<Commande> FindAll() { throw new NotImplementedException(); }
        public List<Commande> FindBySelection(string criteres) { throw new NotImplementedException(); }
    }
}