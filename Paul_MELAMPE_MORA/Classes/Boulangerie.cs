using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Boulangerie
    {
        private string nom;
        private ObservableCollection<Employe> lesEmployes;
        private ObservableCollection<Produit> lesProduits;

        public Boulangerie(): this("")
        {
            this.LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
        }

        public Boulangerie(string nom)
        {
            this.Nom = nom;
            this.LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
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

        public ObservableCollection<Employe> LesEmployes
        {
            get
            {
                return this.lesEmployes;
            }

            set
            {
                this.lesEmployes = value;
            }
        }

        public ObservableCollection<Produit> LesProduits
        {
            get
            {
                return this.lesProduits;
            }

            set
            {
                this.lesProduits = value;
            }
        }
    }
}
