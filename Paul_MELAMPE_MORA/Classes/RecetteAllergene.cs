using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class RecetteAllergene
    {
        private Allergenes allergene;
        private Recette recette;

        public RecetteAllergene()
        {
        }

        public RecetteAllergene(Allergenes allergene, Recette recette)
        {
            this.Allergene = allergene;
            this.Recette = recette;
        }

        public Allergenes Allergene
        {
            get
            {
                return this.allergene;
            }

            set
            {
                this.allergene = value;
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
    }
}
