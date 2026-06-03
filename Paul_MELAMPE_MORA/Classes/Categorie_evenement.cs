using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public enum CategoriePrincipale
    {
        Familial,
        Professionnel
    }

    public class Categorie_evenement : ICrud<Categorie_evenement>
    {
        public static readonly Dictionary<CategoriePrincipale, List<string>> SousCategories = new Dictionary<CategoriePrincipale, List<string>>()
        {
            {
                CategoriePrincipale.Familial,
                new List<string> { "Mariage", "Anniversaire", "Fête", "Religieux", "Naissance" }
            },
            {
                CategoriePrincipale.Professionnel,
                new List<string> { "Réunion", "Pot de départ", "Portes ouvertes" }
            }
        };

        private int categorie_evenement_id;
        private string categorie_evenement_nom;

        public Categorie_evenement()
        {
        }

        public Categorie_evenement(string categorie_evenement_nom)
        {
            this.Categorie_evenement_nom = categorie_evenement_nom;
        }

        public Categorie_evenement(int categorie_evenement_id, string categorie_evenement_nom)
        {
            this.Categorie_evenement_id = categorie_evenement_id;
            this.Categorie_evenement_nom = categorie_evenement_nom;
        }

        public int Categorie_evenement_id
        {
            get
            {
                return this.categorie_evenement_id;
            }

            set
            {
                this.categorie_evenement_id = value;
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
