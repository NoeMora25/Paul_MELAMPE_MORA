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
    public class Recette : ICrud<Recette>
    {
        private int recette_id;
        private string recette_nom;
        private string recette_description;
        private Categorie categorie;
        private string allergenes;

        public Recette()
        {
        }
        public Recette(int recette_id, string recette_nom, string description, Categorie categorie, string allergenes)
        {
            this.recette_id = recette_id;
            this.Recette_nom = recette_nom;
            this.Recette_description = description;
            this.Categorie = categorie;
            this.Allergenes = allergenes;
        }

        public Recette(string recette_nom, string recette_description, Categorie categorie)
        {
            this.Recette_nom = recette_nom;
            this.Recette_description = recette_description;
            this.Categorie = categorie;
        }

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

        public Categorie Categorie
        {
            get
            {
                return this.categorie;
            }

            set
            {
                this.categorie = value;
            }
        }

        public string Allergenes
        {
            get
            {
                return this.allergenes;
            }

            set
            {
                this.allergenes = value;
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
