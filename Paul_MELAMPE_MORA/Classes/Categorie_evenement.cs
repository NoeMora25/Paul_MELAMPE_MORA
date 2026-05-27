using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Categorie_evenement : ICrud<Categorie_evenement>
    {
        private int categorie_evenement_id;
        private string categorie_evenement_nom;

        public int Categorie_evenement_id
        {
            get
            {
                return this.categorie_evenement_id;
            }


        }

        public string Categorie_evenement_nom
        {
            get
            {
                return this.categorie_evenement_nom;
            }

            set
            {
                this.categorie_evenement_nom = value;
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

        public List<Categorie_evenement> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Categorie_evenement> FindBySelection(string criteres)
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
