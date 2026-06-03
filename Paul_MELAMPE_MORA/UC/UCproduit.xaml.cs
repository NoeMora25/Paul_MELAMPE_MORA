using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCproduit : UserControl
    {
        public ObservableCollection<Produit> LesProduits { get; set; }
        public ObservableCollection<LigneCommande> LePanier { get; set; }

        public UCproduit()
        {
            InitializeComponent();

            this.DataContext = this;
            LesProduits = new ObservableCollection<Produit>();
            LePanier = new ObservableCollection<LigneCommande>();

            ChargerLesProduits();
        }

        private void ChargerLesProduits()
        {
            LesProduits.Clear();
            Produit monProduit = new Produit();
            var listeBDD = monProduit.FindAll();

            foreach (var p in listeBDD)
            {
                if (!p.Est_indisponible)
                {
                    LesProduits.Add(p);
                }
            }
        }

        private bool FiltrerLesProduits(object obj)
        {
            Produit leProduit = obj as Produit;
            if (leProduit == null || leProduit.Recette == null) return false;

            bool matchCategorie = true;
            bool unFiltreActifCategorie = (chkCatGateaux.IsChecked == true || chkCatViennoiseries.IsChecked == true || chkCatPains.IsChecked == true);

            if (unFiltreActifCategorie)
            {
                matchCategorie = false;
                string nomCat = leProduit.Recette.Categorie != null ? leProduit.Recette.Categorie.Categorie_nom : "";

                if (chkCatGateaux.IsChecked == true && nomCat == "Gâteaux") matchCategorie = true;
                else if (chkCatViennoiseries.IsChecked == true && nomCat == "Viennoiseries") matchCategorie = true;
                else if (chkCatPains.IsChecked == true && nomCat == "Pains") matchCategorie = true;
            }

            bool matchAllergene = true;
            bool unFiltreActifAllergene = (chkAleGluten.IsChecked == true || chkAleOeuf.IsChecked == true || chkAleLactose.IsChecked == true
                                        || chkAleFruitsACoque.IsChecked == true || chkAleSoja.IsChecked == true
                                        || chkAleSesame.IsChecked == true || chkAleSulfite.IsChecked == true);

            if (unFiltreActifAllergene)
            {
                string listeAllergenes = leProduit.Recette.Allergenes ?? "";

                if (chkAleGluten.IsChecked == true && listeAllergenes.Contains("Céréales contenant du gluten")) matchAllergene = false;
                if (chkAleOeuf.IsChecked == true && listeAllergenes.Contains("Œufs")) matchAllergene = false;
                if (chkAleLactose.IsChecked == true && listeAllergenes.Contains("Lait")) matchAllergene = false;
                if (chkAleFruitsACoque.IsChecked == true && listeAllergenes.Contains("Fruits à coque")) matchAllergene = false;
                if (chkAleSoja.IsChecked == true && listeAllergenes.Contains("Soja")) matchAllergene = false;
                if (chkAleSesame.IsChecked == true && listeAllergenes.Contains("Graines de sésame")) matchAllergene = false;
                if (chkAleSulfite.IsChecked == true && listeAllergenes.Contains("Sulfites")) matchAllergene = false;
            }

            bool matchTexte = RechercheMotClef(obj);

            return matchCategorie && matchAllergene && matchTexte;
        }

        private bool RechercheMotClef(object obj)
        {
            if (String.IsNullOrEmpty(txtBoxProduit.Text))
                return true;

            Produit unProduit = obj as Produit;
            return (unProduit.Recette.Recette_nom.Contains(txtBoxProduit.Text, StringComparison.OrdinalIgnoreCase));
        }

        private void Filtre_Modifie(object sender, RoutedEventArgs e)
        {
            if (icProduits != null && icProduits.ItemsSource != null)
            {
                var vue = CollectionViewSource.GetDefaultView(icProduits.ItemsSource);
                vue.Filter = FiltrerLesProduits;
                vue.Refresh();
            }
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel parent = VisualTreeHelper.GetParent(btn) as StackPanel;
            if (parent != null)
            {
                TextBlock txtQty = parent.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "txtQuantiteSelectionnee");
                if (txtQty != null)
                {
                    int qty = int.Parse(txtQty.Text);
                    qty++;
                    txtQty.Text = qty.ToString();
                }
            }
        }

        private void BtnMoins_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel parent = VisualTreeHelper.GetParent(btn) as StackPanel;
            if (parent != null)
            {
                TextBlock txtQty = parent.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "txtQuantiteSelectionnee");
                if (txtQty != null)
                {
                    int qty = int.Parse(txtQty.Text);
                    if (qty > 0)
                    {
                        qty--;
                        txtQty.Text = qty.ToString();
                    }
                }
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Produit produitClique = btn.DataContext as Produit;
            StackPanel parent = VisualTreeHelper.GetParent(btn) as StackPanel;

            if (produitClique != null && parent != null)
            {
                TextBlock txtQty = parent.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "txtQuantiteSelectionnee");
                if (txtQty != null)
                {
                    int quantiteAajouter = int.Parse(txtQty.Text);
                    if (quantiteAajouter <= 0) return;

                    LigneCommande ligneExistante = LePanier.FirstOrDefault(l => l.LeProduit != null && l.LeProduit.Produit_id == produitClique.Produit_id);

                    if (ligneExistante != null)
                    {
                        ligneExistante.Quantite += quantiteAajouter;
                        int index = LePanier.IndexOf(ligneExistante);
                        LePanier[index] = ligneExistante;
                    }
                    else
                    {
                        LigneCommande nouvelleLigne = new LigneCommande(quantiteAajouter, false, null, produitClique);
                        LePanier.Add(nouvelleLigne);
                    }
                    txtQty.Text = "0";
                }
            }
        }

        private void BtnVoirPanier_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            UCcommande ecranCommande = new UCcommande();
            ecranCommande.LePanier = this.LePanier;
            ecranCommande.icPanierFinal.ItemsSource = ecranCommande.LePanier;
            ecranCommande.RafraichirAffichage();
            mainWindow.MainContent.Content = ecranCommande;
        }
    }
}