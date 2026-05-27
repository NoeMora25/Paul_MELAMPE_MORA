using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Categorie_evenement
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
        public void AjouterCategorie_Evenement()
        {

        }
        public void ModifierCategorie_Evenement()
        {

        }

        public void SupprimerCategorie_Evenement()
        {

        }
    }
}
