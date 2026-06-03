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

        private DateOnly? date_evenement;
        private int? nb_personne;

        private Client client;
        private Categorie_evenement categorie_evenement;

        public Commande()
        {
        }

        public Commande(DateOnly date_creation, DateOnly date_retrait, decimal acompte, bool est_prete, bool est_recuperee, decimal total, DateOnly? date_evenement, int? nb_personne, Client client, Categorie_evenement categorie_evenement)
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

        // Ajout du "?" dans les paramètres
        public Commande(int id, DateOnly date_creation, DateOnly date_retrait, decimal acompte, bool est_prete, bool est_recuperee, decimal total, DateOnly? date_evenement, int? nb_personne, Client client, Categorie_evenement categorie_evenement)
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

        public int Id
        {
            get { return this.commande_id; }
            set { this.commande_id = value; }
        }

        public DateOnly Date_creation
        {
            get { return this.date_creation; }
            set { this.date_creation = value; }
        }

        public DateOnly Date_retrait
        {
            get { return this.date_retrait; }
            set { this.date_retrait = value; }
        }

        public decimal Acompte
        {
            get { return this.acompte; }
            set { this.acompte = value; }
        }

        public bool Est_prete
        {
            get { return this.est_prete; }
            set { this.est_prete = value; }
        }

        public bool Est_recuperee
        {
            get { return this.est_recuperee; }
            set { this.est_recuperee = value; }
        }

        public decimal Total
        {
            get { return this.total; }
            set { this.total = value; }
        }

        // Ajout du "?"
        public DateOnly? Date_evenement
        {
            get { return this.date_evenement; }
            set { this.date_evenement = value; }
        }

        // Ajout du "?"
        public int? Nb_personne
        {
            get { return this.nb_personne; }
            set { this.nb_personne = value; }
        }

        public Client Client
        {
            get { return this.client; }
            set { this.client = value; }
        }

        public Categorie_evenement Categorie_evenement
        {
            get { return this.categorie_evenement; }
            set { this.categorie_evenement = value; }
        }

        public void CalculerAcompte()
        {
        }

        public void CalculerTotal()
        {
        }

        public int Create()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            string sql = @"
            UPDATE commande
            SET 
                categorie_evenement_id = @categorie_evenement_id,
                client_id = @client_id,
                date_creation = @date_creation,
                date_retrait = @date_retrait,
                acompte = @acompte,
                est_prete = @est_prete,
                est_recuperee = @est_recuperee,
                total = @total,
                date_evenement = @date_evenement,
                nb_personne = @nb_personne
            WHERE commande_id = @commande_id;";

            using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(sql))
            {
                cmdUpdate.Parameters.AddWithValue("@commande_id", this.Id);

                // Sécurisation : si la catégorie est null, on envoie DBNull.Value à la base de données
                cmdUpdate.Parameters.AddWithValue("@categorie_evenement_id", this.Categorie_evenement != null ? (object)this.Categorie_evenement.Categorie_evenement_id : DBNull.Value);

                cmdUpdate.Parameters.AddWithValue("@client_id", this.Client.Client_id);
                cmdUpdate.Parameters.AddWithValue("@date_creation", this.Date_creation);
                cmdUpdate.Parameters.AddWithValue("@date_retrait", this.Date_retrait);
                cmdUpdate.Parameters.AddWithValue("@acompte", this.Acompte);
                cmdUpdate.Parameters.AddWithValue("@est_prete", this.Est_prete);
                cmdUpdate.Parameters.AddWithValue("@est_recuperee", this.Est_recuperee);
                cmdUpdate.Parameters.AddWithValue("@total", this.Total);

                // Sécurisation : si Date_evenement ou Nb_personne sont null, on envoie DBNull.Value
                cmdUpdate.Parameters.AddWithValue("@date_evenement", this.Date_evenement.HasValue ? (object)this.Date_evenement.Value : DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@nb_personne", this.Nb_personne.HasValue ? (object)this.Nb_personne.Value : DBNull.Value);

                try
                {
                    int lignesModifiees = DataAccess.ExecuteSet(cmdUpdate);
                    return lignesModifiees;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur lors de la mise à jour de la commande : " + ex.Message);
                }
            }
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();

            string sql = @"
            SELECT c.commande_id, c.date_creation, c.date_retrait, c.acompte, 
                   c.est_prete, c.est_recuperee, c.total, c.date_evenement, c.nb_personne, 
                   cl.client_id, cl.nom, cl.prenom, cl.telephone, cl.mail, 
                   e.categorie_evenement_id, e.categorie_evenement_nom
            FROM commande c
            LEFT JOIN client cl ON c.client_id = cl.client_id
            LEFT JOIN categorie_evenement e ON c.categorie_evenement_id = e.categorie_evenement_id;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Client unClient = new Client(
                        (int)dr["client_id"],
                        dr["nom"].ToString(),
                        dr["prenom"] != DBNull.Value ? dr["prenom"].ToString() : "",
                        dr["telephone"].ToString(),
                        dr["mail"] != DBNull.Value ? dr["mail"].ToString() : ""
                    );

                    Categorie_evenement uneCategorieEvenement = null;
                    if (dr["categorie_evenement_id"] != DBNull.Value)
                    {
                        uneCategorieEvenement = new Categorie_evenement(
                            (int)dr["categorie_evenement_id"],
                            dr["categorie_evenement_nom"].ToString()
                        );
                    }

                    DateOnly? dateEvt = dr["date_evenement"] != DBNull.Value ? (DateOnly)dr["date_evenement"] : null;
                    int? nbPers = dr["nb_personne"] != DBNull.Value ? (int)dr["nb_personne"] : null;

                    Commande laCommande = new Commande(
                        (int)dr["commande_id"],
                        (DateOnly)dr["date_creation"],
                        (DateOnly)dr["date_retrait"],
                        (decimal)dr["acompte"],
                        (bool)dr["est_prete"],
                        (bool)dr["est_recuperee"],
                        (decimal)dr["total"],
                        dateEvt,
                        nbPers,
                        unClient,
                        uneCategorieEvenement
                    );

                    lesCommandes.Add(laCommande);
                }
            }
            return lesCommandes;
        }

        public List<Commande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}