using Paul_MELAMPE_MORA.Classes;
using System.Windows;

namespace Paul_MELAMPE_MORA
{
    public partial class FenetreCreeClient : Window
    {
        public Client NouveauClientCree { get; private set; }

        public FenetreCreeClient()
        {
            InitializeComponent();
        }

        private string FormaterPropre(string texte)
        {
            if (string.IsNullOrWhiteSpace(texte))
                return string.Empty;

            texte = texte.Trim();

            if (texte.Length == 1)
                return texte.ToUpper();

            return char.ToUpper(texte[0]) + texte.Substring(1).ToLower();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCreer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNouveauNom.Text) || string.IsNullOrWhiteSpace(txtNouveauTel.Text))
            {
                MessageBox.Show("Les champs 'Nom' et 'Téléphone' sont obligatoires pour créer un client.", "Information manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client nouveauClient = new Client
            {
                Nom = FormaterPropre(txtNouveauNom.Text),
                Prenom = FormaterPropre(txtNouveauPrenom.Text),
                Telephone = txtNouveauTel.Text.Trim(),
                Mail = txtNouveauMail.Text.Trim()
            };

            try
            {

                nouveauClient.Create();

                NouveauClientCree = nouveauClient;

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message, "Erreur BDD", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}