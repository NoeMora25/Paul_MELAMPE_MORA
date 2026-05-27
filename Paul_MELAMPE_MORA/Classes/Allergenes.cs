using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Allergenes
    {
        private int allergene_id;
        private string allergene_nom;

        public int Allergene_id
        {
            get
            {
                return this.allergene_id;
            }

        }

        public string Allergene_nom
        {
            get
            {
                return this.allergene_nom;
            }

            set
            {
                this.allergene_nom = value;
            }
        }
        public void AjouterAllergene()
        {

        }

        public void ModifierAllergene()
        {

        }

        public void SupprimerAllergene()
        {

        }
    }
}
