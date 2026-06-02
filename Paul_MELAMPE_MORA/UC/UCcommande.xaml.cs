using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCcommande : UserControl
    {
        public ObservableCollection<LigneCommande> LePanier { get; set; }

        public UCcommande()
        {
            InitializeComponent();
            this.DataContext = this;
            LePanier = new ObservableCollection<LigneCommande>();
        }

        private void BtnMoinsUn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                if (ligne.Quantite > 1)
                {
                    ligne.Quantite--;
                    int index = LePanier.IndexOf(ligne);
                    LePanier[index] = ligne;
                }
                else
                {
                    LePanier.Remove(ligne);
                }
                RafraichirAffichage();
            }
        }

        private void BtnPlusUn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                ligne.Quantite++;
                int index = LePanier.IndexOf(ligne);
                LePanier[index] = ligne;
                RafraichirAffichage();
            }
        }

        private void BtnSupprimerLigne_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                LePanier.Remove(ligne);
                RafraichirAffichage();
            }
        }

        private void BtnViderTout_Click(object sender, RoutedEventArgs e)
        {
            LePanier.Clear();
            RafraichirAffichage();
        }
        public void RafraichirAffichage()
        {
            if (icPanierFinal != null && icPanierFinal.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(icPanierFinal.ItemsSource).Refresh();
            }
            CalculerTotaux();
        }

        private void BtnRechercherClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new UCrechercheClient();
        }

        private void BtnValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Commande enregistrée avec succès !");
        }

        private void BtnRetourCatalogue_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new UCproduit();
        }

        private void CalculerTotaux()
        {
            decimal total = 0;
            foreach (var ligne in LePanier)
            {
                if (ligne.LeProduit != null)
                {
                    total += ligne.LeProduit.Prix * ligne.Quantite;
                }
            }

            decimal acompte = total * 0.20m;

            lblTotal.Text = string.Format("{0:N2} €", total);
            lblAcompte.Text = string.Format("{0:N2} €", acompte);
        }
    }
}