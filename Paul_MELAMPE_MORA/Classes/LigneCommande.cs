using Npgsql;
using System;

namespace Paul_MELAMPE_MORA.Classes
{
    public class LigneCommande
    {
        public int Quantite { get; set; }
        public bool Est_decoupe { get; set; }
        public Commande LaCommande { get; set; }
        public Produit LeProduit { get; set; }
        public int Commande_id { get; set; }

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
    }
}