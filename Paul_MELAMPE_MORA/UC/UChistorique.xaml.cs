using Paul_MELAMPE_MORA.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UChistorique : UserControl
    {
        public ObservableCollection<Commande> LesCommandes { get; set; }

        public UChistorique()
        {
            InitializeComponent();
            LesCommandes = new ObservableCollection<Commande>();
            this.DataContext = this;
            dateFiltre.SelectedDate = DateTime.Today;
        }
        public UChistorique(Client clientSpecifique)
        {
            InitializeComponent();
            LesCommandes = new ObservableCollection<Commande>();
            this.DataContext = this;

            ChargerHistoriqueClient(clientSpecifique);
        }
        private void ChargerHistoriqueClient(Client clientSpecifique)
        {
            LesCommandes.Clear();
            Commande lesCommandes = new Commande();

            var listeToutesCommandes = lesCommandes.FindAll();

            foreach (var cmd in listeToutesCommandes)
            {
                if (cmd.Client != null && cmd.Client.Client_id == clientSpecifique.Client_id)
                {
                    LesCommandes.Add(cmd);
                }
            }
        }

        private void ChargerLesCommandesDuJour(DateTime dateChoisie)
        {
            LesCommandes.Clear();
            Commande commandeDuJour = new Commande();
            var listeToutesCommandes = commandeDuJour.FindAll();

            DateOnly dateCible = DateOnly.FromDateTime(dateChoisie);

            foreach (var cmd in listeToutesCommandes)
            {
                if (cmd.Date_retrait == dateCible)
                {
                    LesCommandes.Add(cmd);
                }
            }
        }

        private void DateFiltre_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateFiltre.SelectedDate.HasValue)
            {
                ChargerLesCommandesDuJour(dateFiltre.SelectedDate.Value);
            }
        }

        private void BtnRestituer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Commande commandeCliquee = btn.DataContext as Commande;

            if (commandeCliquee != null)
            {
                commandeCliquee.Est_recuperee = true;

                try
                {
                    commandeCliquee.Update();

                    if (icHistorique != null && icHistorique.ItemsSource != null)
                    {
                        CollectionViewSource.GetDefaultView(icHistorique.ItemsSource).Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la restitution : " + ex.Message, "Erreur BDD", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}