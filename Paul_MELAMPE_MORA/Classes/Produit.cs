using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD3_BindingBDPension.Model;

namespace Paul_MELAMPE_MORA.Classes
{

    public class Produit : ICrud<Produit>
    {
        private int produit_id;
        private bool est_indisponible;
        private int nb_parts;
        private decimal prix;
        private Recette recette;

        public Produit()
        {
        }


        public Produit(bool est_disponible, int nb_parts, decimal prix, Recette recette)
        {
            this.Est_indisponible = est_disponible;
            this.Nb_parts = nb_parts;
            this.Prix = prix;
            this.Recette = recette;
        }
        public Produit(int produit_id, Recette recette, bool est_indisponible, int nb_parts, decimal prix)
        {
            this.Produit_id = produit_id;
            this.Recette = recette;
            this.Est_indisponible = est_indisponible;
            this.Nb_parts = nb_parts;
            this.Prix = prix;
        }

        public int Produit_id   
        {
            get
            {
                return this.produit_id;
            }
            set
            {
                this.produit_id = value;
            }

        }

        public bool Est_indisponible
        {
            get
            {
                return this.est_indisponible;
            }

            set
            {
                this.est_indisponible = value;
            }
        }

        public int Nb_parts
        {
            get
            {
                return this.nb_parts;
            }

            set
            {
                this.nb_parts = value;
            }
        }

        public decimal Prix
        {
            get
            {
                return this.prix;
            }

            set
            {
                this.prix = value;
            }
        }

        public Recette Recette
        {
            get
            {
                return this.recette;
            }

            set
            {
                this.recette = value;
            }
        }

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Produit> FindAll()
        {
            List<Produit> lesProduits = new List<Produit>();

            string sql = @"
            SELECT p.produit_id, p.est_indisponible, p.nb_parts, p.prix, r.recette_id, r.recette_nom, r.recette_description, c.categorie_id, c.categorie_nom,
            STRING_AGG(a.allergene_nom, ', ') AS liste_allergenes
            FROM produit p
            INNER JOIN recette r ON p.recette_id = r.recette_id
            INNER JOIN categorie c ON r.categorie_id = c.categorie_id
            LEFT JOIN recette_allergene ra ON r.recette_id = ra.recette_id
            LEFT JOIN allergene a ON ra.allergene_id = a.allergene_id
            GROUP BY p.produit_id, p.est_indisponible, p.nb_parts, p.prix, r.recette_id, r.recette_nom, r.recette_description, c.categorie_id, c.categorie_nom;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Categorie laCategorie = new Categorie(
                        (int)dr["categorie_id"],
                        dr["categorie_nom"].ToString()
                    );

                    string phraseAllergenes = dr["liste_allergenes"] == DBNull.Value ? "Aucun" : dr["liste_allergenes"].ToString();

                    Recette laRecette = new Recette(
                        (int)dr["recette_id"],
                        dr["recette_nom"].ToString(),
                        dr["recette_description"].ToString(),
                        laCategorie,
                        phraseAllergenes
                    );

                    Produit leProduit = new Produit(
                        (int)dr["produit_id"],
                        laRecette,
                        (bool)dr["est_indisponible"],
                        (int)dr["nb_parts"],
                        (decimal)dr["prix"]
                    );

                    lesProduits.Add(leProduit);
                }
            }
            return lesProduits;
        }

        public List<Produit> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
