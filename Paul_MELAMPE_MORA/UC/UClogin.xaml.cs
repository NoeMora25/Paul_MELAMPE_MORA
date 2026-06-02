using Paul_MELAMPE_MORA.Classes;
using System;
using System.Linq;
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
            mainWindow.mdp_user = TxtMDP.Text;
            mainWindow.loginuser = TxtLogin.Text;

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
                    }
                    else if (mainWindow.roleUser == "Vendeur")
                    {
                        mainWindow.MainContent.Content = new UCrechercheClient();
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