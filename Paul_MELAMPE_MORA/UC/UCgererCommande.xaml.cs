using Paul_MELAMPE_MORA.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
            ChargerLesCommandes();
        }
        private void ChargerLesCommandes()
        {
            LesCommandes.Clear();
            Commande uneCommande = new Commande();
            var listeCommandes = uneCommande.FindAll();

            foreach (var elt in listeCommandes)
            {
                if (!elt.Est_recuperee)
                {
                    LesCommandes.Add(elt);
                }
            }
        }

        private void BtnFinaliser_Click(object sender, RoutedEventArgs e)
        {
            Button boutonClique = sender as Button;

            if (boutonClique != null && boutonClique.DataContext is Commande commandeSelectionnee)
            {
                try
                {
                    commandeSelectionnee.Est_prete = true;

                    commandeSelectionnee.Update();

                    if (this.DataContext is Boulangerie maBoulangerie)
                    {
                        maBoulangerie.LesCommandes.Clear();

                        Commande uneCommande = new Commande();
                        var listeCommandes = uneCommande.FindAll();

                        foreach (var elt in listeCommandes)
                        {
                            maBoulangerie.LesCommandes.Add(elt);
                        }
                    }

                    MessageBox.Show("La commande a été marquée comme prête !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la mise à jour en base de données : " + ex.Message, "Erreur BDD", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button boutonClique = sender as Button;

            if (boutonClique != null && boutonClique.DataContext is Commande commandeChoisie)
            {
                UCcommande uneCommandeUC = new UCcommande();

                uneCommandeUC.ChargerPourModification(commandeChoisie);

                Window popupCommande = new Window
                {
                    Title = "Modifier la commande de " + commandeChoisie.Client.Nom,
                    Content = uneCommandeUC,
                    Width = 1000,
                    Height = 750,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStyle = WindowStyle.ToolWindow,
                };

                popupCommande.ShowDialog();

                ChargerLesCommandes();
            }
        }


    }

}
