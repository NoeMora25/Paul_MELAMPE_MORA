using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Commande : ICrud<Commande>
    {
        private int commande_id;
        private DateTime date_creation;
        private DateTime date_retrait;
        private decimal acompte;
        private bool est_prete;
        private bool est_recuperee;
        private decimal total;
        private DateTime date_evenement;
        private int nb_personne;

        public Commande(DateTime date_creation, DateTime date_retrait, decimal acompte, bool est_prete, bool est_recuperee, decimal total, DateTime date_evenement, int nb_personne)
        {
            this.Date_creation = date_creation;
            this.Date_retrait = date_retrait;
            this.Acompte = acompte;
            this.Est_prete = est_prete;
            this.Est_recuperee = est_recuperee;
            this.Total = total;
            this.Date_evenement = date_evenement;
            this.Nb_personne = nb_personne;
        }

        public Commande()
        {
        }

        public int Id
        {
            get
            {
                return this.commande_id;
            }

        }

        public DateTime Date_creation
        {
            get
            {
                return this.date_creation;
            }

            set
            {
                this.date_creation = value;
            }
        }

        public DateTime Date_retrait
        {
            get
            {
                return this.date_retrait;
            }

            set
            {
                this.date_retrait = value;
            }
        }

        public decimal Acompte
        {
            get
            {
                return this.acompte;
            }

            set
            {
                this.acompte = value;
            }
        }

        public bool Est_prete
        {
            get
            {
                return this.est_prete;
            }

            set
            {
                this.est_prete = value;
            }
        }

        public bool Est_recuperee
        {
            get
            {
                return this.est_recuperee;
            }

            set
            {
                this.est_recuperee = value;
            }
        }

        public decimal Total
        {
            get
            {
                return this.total;
            }

            set
            {
                this.total = value;
            }
        }

        public DateTime Date_evenement
        {
            get
            {
                return this.date_evenement;
            }

            set
            {
                this.date_evenement = value;
            }
        }

        public int Nb_personne
        {
            get
            {
                return this.nb_personne;
            }

            set
            {
                this.nb_personne = value;
            }
        }


        public void CalculerAcompte()
        {

        }
        public void CalculerTotal()
        {

        }

        public int Create()
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

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Commande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
