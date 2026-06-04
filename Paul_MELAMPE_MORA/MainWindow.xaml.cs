using Paul_MELAMPE_MORA.Classes;
using Paul_MELAMPE_MORA.UC;
using System.Windows;

namespace Paul_MELAMPE_MORA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Boulangerie LaBoulangerie { get; set; }
        public string roleUser;
        public string mdp_user;
        public string loginuser;
        public MainWindow()
        {

            InitializeComponent();

            MainContent.Content = new UC.UClogin();


        }


        public void ChargeData()
        {
            try
            {
                LaBoulangerie = new Boulangerie("Boulangerie Paul");
                this.DataContext = LaBoulangerie;
            }
            catch (Exception ex)
            {
                Npgsql.NpgsqlConnection.ClearAllPools();
                throw new Exception("Identifiant ou mot de passe invalide");
                //MessageBox.Show("Impossible de charger les données. Voir votre admin.");
                //Application.Current.Shutdown();

            }
        }

        private void ButDeconnecter_Click(object sender, RoutedEventArgs e)
        {
            mdp_user = "";
            loginuser = "";
            MainContent.Content = new UClogin();
            MenuVendeur.Visibility = Visibility.Collapsed;
            ButDeconnecter.Visibility = Visibility.Collapsed;
        }
        private void BtnMenuCommande_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UCproduit();
        }

        private void BtnMenuClient_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UCrechercheClient(false);
        }
        private void BtnMenuHistorique_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UChistorique();
        }
    }
}