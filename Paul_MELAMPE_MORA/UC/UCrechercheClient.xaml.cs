using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCrechercheClient : UserControl
    {
        public ObservableCollection<Client> LesClients { get; set; }

        public UCrechercheClient()
        {
            InitializeComponent();
            this.DataContext = this;
            LesClients = new ObservableCollection<Client>();
            ChargerLesClients();
        }

        private void ChargerLesClients()
        {
            LesClients.Clear();
            Client monClient = new Client();
            var listeBDD = monClient.FindAll();
            foreach (var c in listeBDD)
            {
                LesClients.Add(c);
            }
        }

        private void Filtre_Modifie(object sender, TextChangedEventArgs e)
        {
            if (icClients != null && icClients.ItemsSource != null)
            {
                var vue = CollectionViewSource.GetDefaultView(icClients.ItemsSource);
                vue.Filter = FiltrerLesClients;
                vue.Refresh();
            }
        }

        private bool FiltrerLesClients(object obj)
        {
            Client leClient = obj as Client;
            if (leClient == null) return false;

            bool matchNom = string.IsNullOrEmpty(txtNom.Text) || leClient.Nom.Contains(txtNom.Text, StringComparison.OrdinalIgnoreCase);
            bool matchPrenom = string.IsNullOrEmpty(txtPrenom.Text) || leClient.Prenom.Contains(txtPrenom.Text, StringComparison.OrdinalIgnoreCase);
            bool matchTel = string.IsNullOrEmpty(txtTel.Text) || leClient.Telephone.Contains(txtTel.Text, StringComparison.OrdinalIgnoreCase);
            bool matchMail = string.IsNullOrEmpty(txtMail.Text) || leClient.Mail.Contains(txtMail.Text, StringComparison.OrdinalIgnoreCase);

            return matchNom && matchPrenom && matchTel && matchMail;
        }

        private void BtnSelectionner_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Client clientSelectionne = btn.DataContext as Client;
            if (clientSelectionne != null)
            {
                MessageBox.Show($"Client sélectionné : {clientSelectionne.Prenom} {clientSelectionne.Nom}");
            }
        }
    }
}