using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class LigneCommande : ICrud<LigneCommande>
    {
        private int quantite;
        private bool est_decoupe;
        private Commande commande;
        private Produit leProduit;

        public LigneCommande()
        {
        }

        public LigneCommande(int quantite, bool est_decoupe, Commande commande, Produit leProduit)
        {
            this.Quantite = quantite;
            this.Est_decoupe = est_decoupe;
            this.Commande = commande;
            this.LeProduit = leProduit;
        }

        public int Quantite
        {
            get
            {
                return this.quantite;
            }

            set
            {
                this.quantite = value;
            }
        }

        public bool Est_decoupe
        {
            get
            {
                return this.est_decoupe;
            }

            set
            {
                this.est_decoupe = value;
            }
        }

        public Commande Commande
        {
            get
            {
                return this.commande;
            }

            set
            {
                this.commande = value;
            }
        }

        public Produit LeProduit
        {
            get
            {
                return this.leProduit;
            }

            set
            {
                this.leProduit = value;
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

        public List<LigneCommande> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<LigneCommande> FindBySelection(string criteres)
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
