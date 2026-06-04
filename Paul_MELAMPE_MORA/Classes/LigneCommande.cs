using Npgsql;
using System.Data;

namespace Paul_MELAMPE_MORA.Classes
{
    public class LigneCommande : ICrud<LigneCommande>
    {
        public int Quantite { get; set; }
        public bool Est_decoupe { get; set; }
        public Commande LaCommande { get; set; }
        public Produit LeProduit { get; set; }
        public int Commande_id { get; set; }

        public LigneCommande()
        {
        }

        public LigneCommande(int quantite, bool est_decoupe, Commande laCommande, Produit leProduit)
        {
            this.Quantite = quantite;
            this.Est_decoupe = est_decoupe;
            this.LaCommande = laCommande;
            this.LeProduit = leProduit;
        }

        public int Create()
        {
            string sql = @"INSERT INTO ligne_commande (commande_id, produit_id, quantite, est_decoupe) 
                   VALUES (@commande_id, @produit_id, @quantite, @est_decoupe);";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@commande_id", this.Commande_id);
                cmd.Parameters.AddWithValue("@produit_id", this.LeProduit.Produit_id);
                cmd.Parameters.AddWithValue("@quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@est_decoupe", this.Est_decoupe);

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            string sql = "DELETE FROM ligne_commande WHERE commande_id = @commande_id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@commande_id", this.Commande_id);

                try
                {
                    return DataAccess.ExecuteSet(cmd);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public List<LigneCommande> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<LigneCommande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public List<LigneCommande> FindBySelection(int idCommande)
        {
            List<LigneCommande> listeLignes = new List<LigneCommande>();

            string sql = @"
                select lc.commande_id, lc.produit_id, lc.quantite, lc.est_decoupe,
                       p.nb_parts, p.prix, p.est_indisponible,
                       r.recette_id, r.recette_nom, r.recette_description,
                       c.categorie_id, c.categorie_nom
                from ligne_commande lc
                left JOIN produit p on lc.produit_id = p.produit_id
                left JOIN recette r on p.recette_id = r.recette_id
                left JOIN categorie c on r.categorie_id = c.categorie_id
                where lc.commande_id = @id;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                cmdSelect.Parameters.AddWithValue("@id", idCommande);
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);

                foreach (DataRow dr in dt.Rows)
                {
                    Categorie laCat = new Categorie(
                        (int)dr["categorie_id"],
                        dr["categorie_nom"].ToString()
                    );

                    Recette laRecette = new Recette(
                        (int)dr["recette_id"],
                        dr["recette_nom"].ToString(),
                        dr["recette_description"].ToString(),
                        laCat,
                        "Aucun"
                    );

                    Produit leProduit = new Produit(
                        (int)dr["produit_id"],
                        laRecette,
                        (bool)dr["est_indisponible"],
                        (int)dr["nb_parts"],
                        (decimal)dr["prix"]
                    );

                    LigneCommande laLigne = new LigneCommande(
                        (int)dr["quantite"],
                        (bool)dr["est_decoupe"],
                        null,
                        leProduit
                    );
                    laLigne.Commande_id = idCommande;

                    listeLignes.Add(laLigne);
                }
            }
            return listeLignes;
        }
    }
}