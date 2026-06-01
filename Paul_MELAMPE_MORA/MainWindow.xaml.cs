using Paul_MELAMPE_MORA.Classes;
using Paul_MELAMPE_MORA.UC;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                throw new Exception("Identifiant ou mot de passe invalide");
                //MessageBox.Show("Impossible de charger les données. Voir votre admin.");
                //Application.Current.Shutdown();

            }
        }






    }
}