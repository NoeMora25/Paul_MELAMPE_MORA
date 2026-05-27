using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{

    public class Produit : ICrud<Produit>
    {
        private int produit_id;
        private bool est_disponible;
        private int nb_parts;
        private decimal prix;
        private Recette recette;

        public Produit()
        {
        }

        public Produit(bool est_disponible, int nb_parts, decimal prix, Recette recette)
        {
            this.Est_disponible = est_disponible;
            this.Nb_parts = nb_parts;
            this.Prix = prix;
            this.Recette = recette;
        }

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
            throw new NotImplementedException();
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
