using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paul_MELAMPE_MORA.UC
{
    /// <summary>
    /// Logique d'interaction pour UCgererCommande.xaml
    /// </summary>
    public partial class UCgererCommande : UserControl
    {
        public ObservableCollection<Commande> LesCommandes { get; set; }
        public UCgererCommande()
        {
            InitializeComponent();
            LesCommandes = new ObservableCollection<Commande>();
            //this.DataContext = LesCommandes;
            ChargerLesCommandes();
        }
        private void ChargerLesCommandes()
        {
            LesCommandes.Clear();
            Commande uneCommande = new Commande();
            var listeCommandes = uneCommande.FindAll();

            foreach (var elt in listeCommandes)
            {
                LesCommandes.Add(elt);
                //if (!elt.Est_prete)
                //{
                //    LesCommandes.Add(elt);
                //}
            }
        }


    }
}
