using System.Collections.ObjectModel;

namespace Paul_MELAMPE_MORA.Classes
{
    public class Boulangerie
    {
        private string nom;
        private ObservableCollection<Employe> lesEmployes;
        private ObservableCollection<Produit> lesProduits;
        private ObservableCollection<Commande> lesCommandes;

        public Boulangerie() : this("")
        {
            this.LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
            this.LesCommandes = new ObservableCollection<Commande>(new Commande().FindAll());
        }

        public Boulangerie(string nom)
        {
            this.Nom = nom;
            this.LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
            this.LesCommandes = new ObservableCollection<Commande>(new Commande().FindAll());
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

        public ObservableCollection<Commande> LesCommandes
        {
            get
            {
                return this.lesCommandes;
            }

            set
            {
                this.lesCommandes = value;
            }
        }
    }
}
