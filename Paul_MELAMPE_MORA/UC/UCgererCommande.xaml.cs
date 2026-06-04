using Paul_MELAMPE_MORA.Classes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                if (!elt.Est_recuperee)
                {
                    LesCommandes.Add(elt);
                }
            }
        }

        private void BtnFinaliser_Click(object sender, RoutedEventArgs e)
        {
            // 1. On récupère le bouton cliqué
            Button boutonClique = sender as Button;

            // 2. On extrait la commande liée à ce bouton
            if (boutonClique != null && boutonClique.DataContext is Commande commandeSelectionnee)
            {
                try
                {
                    // 3. On modifie l'objet en mémoire
                    commandeSelectionnee.Est_prete = true;

                    // 4. On lance la sauvegarde dans la base de données
                    commandeSelectionnee.Update();

                    // 5. On rafraîchit la VRAIE liste affichée à l'écran
                    if (this.DataContext is Boulangerie maBoulangerie)
                    {
                        // On vide la liste regardée par le XAML
                        maBoulangerie.LesCommandes.Clear();

                        // On va rechercher les nouvelles données en base
                        Commande uneCommande = new Commande();
                        var listeCommandes = uneCommande.FindAll();

                        // On reremplit la liste
                        foreach (var elt in listeCommandes)
                        {
                            maBoulangerie.LesCommandes.Add(elt);
                        }
                    }

                    MessageBox.Show("La commande a été marquée comme prête !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // IMPORTANT : Si la BDD refuse la modification (erreur SQL), ce message s'affichera
                    MessageBox.Show("Erreur lors de la mise à jour en base de données : " + ex.Message, "Erreur BDD", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button boutonClique = sender as Button;

            if (boutonClique != null && boutonClique.DataContext is Commande commandeChoisie)
            {
                // 1. On crée le UserControl de la commande
                UCcommande uneCommandeUC = new UCcommande();

                // 2. MAGIE : On lui dit de se pré-remplir avec la commande cliquée !
                uneCommandeUC.ChargerPourModification(commandeChoisie);

                // 3. On ouvre le Pop-up
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

                // 4. Une fois le Pop-up fermé, on rafraîchit la liste principale 
                // au cas où l'utilisateur aurait modifié des prix ou des dates.
                ChargerLesCommandes();
            }
        }
    

    }

}
