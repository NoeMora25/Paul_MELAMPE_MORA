using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour UCproduit.xaml
    /// </summary>
    public partial class UCproduit : UserControl
    {
        public UCproduit()
        {
            InitializeComponent();

        }

        #region Filtre UCproduit
        private bool FiltrerLesProduits(object obj)
        {
            Produit leProduit = obj as Produit;
            if (leProduit == null) return false;

            bool matchCategorie = true;

            bool unFiltreActifCategorie = (chkCatGateaux.IsChecked == true || chkCatViennoiseries.IsChecked == true || chkCatPains.IsChecked == true);

            if (unFiltreActifCategorie)
            {
                matchCategorie = false;

                if (chkCatGateaux.IsChecked == true && leProduit.Recette.Categorie.Categorie_nom == "Gâteaux")
                    matchCategorie = true;

                else if (chkCatViennoiseries.IsChecked == true && leProduit.Recette.Categorie.Categorie_nom == "Viennoiseries")
                    matchCategorie = true;

                else if (chkCatPains.IsChecked == true && leProduit.Recette.Categorie.Categorie_nom == "Pains")
                    matchCategorie = true;
            }

            bool matchAllergene = true;

            bool unFiltreActifAllergene = (chkAleGluten.IsChecked == true || chkAleOeuf.IsChecked == true || chkAleLactose.IsChecked == true 
                                            || chkAleFruitsACoque.IsChecked == true || chkAleSoja.IsChecked == true
                                            || chkAleSesame.IsChecked == true || chkAleSulfite.IsChecked == true);

            if (unFiltreActifAllergene)
            {
                matchAllergene = true;

                string listeAllergenes = leProduit.Recette.Allergenes ?? "";

                if (chkAleGluten.IsChecked == true && leProduit.Recette.Allergenes.Contains("Céréales contenant du gluten"))
                    matchAllergene = false;

                if (chkAleOeuf.IsChecked == true && leProduit.Recette.Allergenes.Contains("Œufs"))
                    matchAllergene = false;

                if (chkAleLactose.IsChecked == true && leProduit.Recette.Allergenes.Contains("Lait"))
                    matchAllergene = false;

                if (chkAleFruitsACoque.IsChecked == true && leProduit.Recette.Allergenes.Contains("Fruits à coque"))
                    matchAllergene = false;

                if (chkAleSoja.IsChecked == true && leProduit.Recette.Allergenes.Contains("Soja"))
                    matchAllergene = false;

                if (chkAleSesame.IsChecked == true && leProduit.Recette.Allergenes.Contains("Graines de sésame"))
                    matchAllergene = false;

                if (chkAleSulfite.IsChecked == true && leProduit.Recette.Allergenes.Contains("Sulfites"))
                    matchAllergene = false;
            }

            //bool matchTexte = true;
            //if (!string.IsNullOrEmpty(txtRecherche.Text))
            //{
            //     matchTexte = leProduit.Recette.Recette_nom.Contains(txtRecherche.Text);
            //}


            return matchCategorie && matchAllergene; // && matchTexte
        }

        #endregion

        private void Filtre_Modifie(object sender, RoutedEventArgs e)
        {
            if (dgProduits != null && dgProduits.ItemsSource != null)
            {
                var vue = CollectionViewSource.GetDefaultView(dgProduits.ItemsSource);
                vue.Filter = FiltrerLesProduits;
                vue.Refresh();
            }
        }

    }
}

