using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace RecetteMediator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public string nameRecette { get => this.recetteName.Text; }
        public string tempsRecette { get => this.recetteTemps.Text; }
        public ObservableCollection<Etape> Etapes = new ObservableCollection<Etape>();
        StackPanel ingredients { get => ingredientList; }

        public MainWindow()
        {
            InitializeComponent();
            
            this.etapesLB.ItemsSource = Etapes;
        }

        // Bouton ajout etape
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (anyCheckedIngredients())
            {
                var etape = new Etape(this.etapDesc.Text, getCheckedIngredients());
                Etapes.Add(etape);
            }
            else
                MessageBox.Show("Vous devez d'abord selectionner un ingrédient");
        }

        private List<string> getCheckedIngredients()
        {
            var list = new List<string>();
            foreach (CheckBox ingredient in ingredients.Children)
            {
                if (ingredient.IsChecked.Value)
                    list.Add(ingredient.Content.ToString());
            }
            return list;
        }

        private bool anyCheckedIngredients()
        {
            return (getCheckedIngredients().Count > 0);
        }

        // Bouton suppression
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void imprimerBouton_Click(object sender, RoutedEventArgs e)
        {
            if (Etapes.Count > 0 && nameRecette.Length > 0 && tempsRecette.Length > 0)
            {
                string afficher = "";
                foreach (var etape in Etapes)
                {
                    afficher += etape + "\n";
                }
                MessageBox.Show(afficher);
            }
            else
                MessageBox.Show("Vous devez encoder : \n - un nom de recette\n - un temps de préparation\n - au moins un étape");
        }
    }
}
