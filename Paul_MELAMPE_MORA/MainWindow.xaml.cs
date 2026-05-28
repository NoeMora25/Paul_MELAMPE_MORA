using Paul_MELAMPE_MORA.Classes;
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
        public MainWindow()
        {
            ChargeData();
            InitializeComponent();
            MainContent.Content = new UC.UCproduit();

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

                MessageBox.Show("Impossible de charger les données. Voir votre admin.");
                Application.Current.Shutdown();
            }
        }

        public bool EmployeExiste(string login)
        {
            foreach (Employe unEmploye in LaBoulangerie.LesEmployes)
            {
                if (login == unEmploye.Login)
                {
                    return true;
                }
            }
            return false;   

        }

       




    }
}