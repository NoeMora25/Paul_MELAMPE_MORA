using Paul_MELAMPE_MORA.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UClogin : UserControl
    {
        public UClogin()
        {
            InitializeComponent();
        }

        private void ButConnecter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mdp_user = TxtMDP.Password.Trim();
            mainWindow.loginuser = TxtLogin.Text.Trim();

            try
            {
                mainWindow.ChargeData();

                Employe unEmploye = mainWindow.LaBoulangerie.LesEmployes.FirstOrDefault(emp => emp.Login == mainWindow.loginuser);

                if (unEmploye != null)
                {
                    mainWindow.roleUser = unEmploye.Role;

                    if (mainWindow.roleUser == "Chef")
                    {
                        mainWindow.MainContent.Content = new UCgererCommande();
                        mainWindow.ButDeconnecter.Visibility = Visibility.Visible;
                    }
                    else if (mainWindow.roleUser == "Vendeur")
                    {
                        mainWindow.MainContent.Content = new UCproduit();
                        mainWindow.MenuVendeur.Visibility = Visibility.Visible;
                        mainWindow.ButDeconnecter.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MsgErreur.Content = "Vous n'avez pas de rôle !";
                    }
                }
                else
                {
                    MsgErreur.Content = "Identifiant introuvable.";
                }
            }
            catch (Exception ex)
            {
                MsgErreur.Content = ex.Message;
            }
        }
    }
}