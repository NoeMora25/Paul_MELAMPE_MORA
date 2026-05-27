using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Client
    {
        private int client_id;
        private string nom;
        private string prenom;
        private string telephone;
        private string mail;

        public int Id
        {
            get
            {
                return this.client_id;
            }


        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Telephone
        {
            get
            {
                return this.telephone;
            }

            set
            {
                this.telephone = value;
            }
        }

        public string Mail
        {
            get
            {
                return this.mail;
            }

            set
            {
                this.mail = value;
            }
        }
        public void AjouterClient()
        {

        }

        public void ModifierClient()
        {

        }

        public void SupprimerClient()
        {

        }
    }
}
