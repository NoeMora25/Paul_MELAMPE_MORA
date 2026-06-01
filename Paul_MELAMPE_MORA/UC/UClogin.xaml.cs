using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Logique d'interaction pour UClogin.xaml
    /// </summary>

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
                foreach (Employe unEmploye in mainWindow.LaBoulangerie.LesEmployes)
                {
                    mainWindow.roleUser = unEmploye.Role;

                    if (mainWindow.roleUser == "Chef")
                        mainWindow.MainContent.Content = new UCgererCommande();

                    else if (mainWindow.roleUser == "Vendeur")
                    {
                        mainWindow.MainContent.Content = new UCcommande();
                    }

                    else
                        MsgErreur.Content = "Vous n'avez pas de rôle !";

                }
            }
            catch (Exception ex)
            {
                MsgErreur.Content = ex.Message;
            }



            



        }


    }
}
