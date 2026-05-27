using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Produit
    {
        private int produit_id;
        private bool est_disponible;
        private int nb_parts;
        private decimal prix;

        public int Id
        {
            get
            {
                return this.produit_id;
            }

        }

        public bool Est_disponible
        {
            get
            {
                return this.est_disponible;
            }

            set
            {
                this.est_disponible = value;
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
        public void AjouterProduit()
        {

        }
        public void ModifierProduit()
        {

        }

        public void SupprimerProduit()
        {

        }
    }
}
