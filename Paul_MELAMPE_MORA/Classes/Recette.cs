using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Recette : ICrud<Recette>
    {
        private int recette_id;
        private string recette_nom;
        private string recette_description;

        public int Recette_id
        {
            get
            {
                return this.recette_id;
            }


        }

        public string Recette_nom
        {
            get
            {
                return this.recette_nom;
            }

            set
            {
                this.recette_nom = value;
            }
        }

        public string Recette_description
        {
            get
            {
                return this.recette_description;
            }

            set
            {
                this.recette_description = value;
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

        public List<Recette> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Recette> FindBySelection(string criteres)
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
