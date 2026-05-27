using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public enum Categorie_produit { Gateaux, Viennoiseries, Pains}
    public class Categorie : ICrud<Categorie>
    {
        private int categorie_id;
        private string categorie_nom;

        public int Categorie_id
        {
            get
            {
                return this.categorie_id;
            }

        }

        public string Categorie_nom
        {
            get
            {
                return this.categorie_nom;
            }

            set
            {
                this.categorie_nom = value;
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

        public List<Categorie> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Categorie> FindBySelection(string criteres)
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
