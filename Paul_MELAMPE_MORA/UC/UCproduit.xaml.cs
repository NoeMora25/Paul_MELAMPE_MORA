using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCproduit : UserControl
    {
        public ObservableCollection<LignePanier> LePanier { get; set; }
        public ObservableCollection<Produit> LesProduits { get; set; }

        public UCproduit()
        {
            InitializeComponent();

            this.DataContext = this;
            LesProduits = new ObservableCollection<Produit>();
            LePanier = new ObservableCollection<LignePanier>();

            ChargerLesProduits();
        }

        private void ChargerLesProduits()
        {
            LesProduits.Clear();
            Produit monDAO = new Produit();
            var listeBDD = monDAO.FindAll();

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

            return matchCategorie && matchAllergene;
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

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Produit produitClique = btn.DataContext as Produit;

            if (produitClique != null)
            {
                LignePanier ligneExistante = LePanier.FirstOrDefault(l => l.LeProduit.Produit_id == produitClique.Produit_id);

                if (ligneExistante != null)
                {
                    ligneExistante.Quantite++;
                    int index = LePanier.IndexOf(ligneExistante);
                    LePanier[index] = ligneExistante;
                }
                else
                {
                    LePanier.Add(new LignePanier { LeProduit = produitClique, Quantite = 1 });
                }
            }
        }

        private void BtnMoins_Click(object sender, RoutedEventArgs e) { }
        private void BtnPlus_Click(object sender, RoutedEventArgs e) { }

        public class LignePanier
        {
            public Produit LeProduit { get; set; }
            public int Quantite { get; set; }
        }
    }
}