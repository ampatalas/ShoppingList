using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ShoppingList.Resources;
using ShoppingList.Logic;
using System.Collections.ObjectModel;

namespace ShoppingList
{
    public partial class MainPage : PhoneApplicationPage
    {
        //public static List<ShoppingItem> shoppingList { get; set; }
        public static ObservableCollection<ShoppingItem> shoppingList { get; set; }
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //new way
            shoppingList = new ObservableCollection<ShoppingItem> {
                new ShoppingItem {Name = "Milk", Category = Category.Diary, Amount = 2},
                new ShoppingItem {Name = "Eggs", Category = Category.Diary, Amount = 7},
                new ShoppingItem {Name = "Tomatoes", Category = Category.Vegetables, Amount = 3},
                new ShoppingItem {Name = "Chicken breast", Category = Category.Meat, Amount = 2},
                new ShoppingItem {Name = "Apples", Category = Category.Fruit, Amount = 5}
            };

            shoppingListBinding.DataContext = shoppingList;
            
            //old way
            
            //shoppingList = new List<ShoppingItem>
            //{
            //    new ShoppingItem {name = "Milk", category = Category.Diary, Amount = 1},
            //    new ShoppingItem {name = "Eggs", category = Category.Diary, Amount = 6},
            //    new ShoppingItem {name = "Tomatoes", category = Category.Vegetables, Amount = 2},
            //    new ShoppingItem {name = "Chicken breast", category = Category.Meat, Amount = 1},
            //    new ShoppingItem {name = "Apples", category = Category.Fruit, Amount = 4}
            //};
           // shoppingListMain.ItemsSource = shoppingListAsStackPanels(shoppingList);
            
        }

        //private System.Collections.IEnumerable shoppingListAsStackPanels(List<ShoppingItem> shoppingList)
        //{
        //    Thickness margin = new Thickness();
        //    margin.Left = 10;

        //    List<Grid> grids = new List<Grid>();
        //    foreach (ShoppingItem item in shoppingList)
        //    {
        //        Grid grid = createNewShoppingListItemGrid(margin, item);
        //        grids.Add(grid);

        //    }
        //    return grids;
        //}

        private static Grid createNewShoppingListItemGrid(Thickness margin, ShoppingItem item)
        {
            Grid grid = new Grid();
            grid.Width = 400;
            RowDefinition row1 = new RowDefinition();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            col1.Width = new GridLength(2.7, GridUnitType.Star);
            col2.Width = new GridLength(1.9, GridUnitType.Star);
            col3.Width = new GridLength(1.2, GridUnitType.Star);
            grid.RowDefinitions.Add(row1);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            grid.ColumnDefinitions.Add(col3);

            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = item.Name;

            TextBlock categoryBlock = new TextBlock();
            categoryBlock.Text = item.Category.ToString();
            categoryBlock.Margin = margin;

            TextBlock amountBlock = new TextBlock();
            amountBlock.Text = item.Amount.ToString();
            amountBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            Grid.SetRow(nameBlock, 0);
            Grid.SetRow(categoryBlock, 0);
            Grid.SetRow(amountBlock, 0);

            Grid.SetColumn(nameBlock, 0);
            Grid.SetColumn(categoryBlock, 1);
            Grid.SetColumn(amountBlock, 2);

            grid.Children.Add(nameBlock);
            grid.Children.Add(categoryBlock);
            grid.Children.Add(amountBlock);

            return grid;
        }

        private void ClickAdd(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddItem", UriKind.Relative));
        }

        private void ClickRemove(object sender, EventArgs e)
        {
            if (shoppingListBinding.SelectedItem == null) MessageBox.Show("No item is selected.", "Deleting items", MessageBoxButton.OK);
            else
            {
                ShoppingItem toDelete = shoppingList[shoppingListBinding.SelectedIndex];
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + toDelete.Name + "?", "Deleting items", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    shoppingList.RemoveAt(shoppingListBinding.SelectedIndex);
                    //refreshList();
                }
            }
        }
        private void ClickEdit(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["EditedItem"] = shoppingListBinding.SelectedItem as ShoppingItem;
            this.NavigationService.Navigate(new Uri("/EditItem", UriKind.Relative));
        }


        //public void refreshList()
        //{
        //    shoppingListBinding.ItemsSource = null;
        //    shoppingListBinding.ItemsSource = shoppingListAsStackPanels(shoppingList);
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //refreshList();
        }

        private void shoppingListBinding_GotFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBarIconButton editButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            ApplicationBarIconButton deleteButton = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            editButton.IsEnabled = true;
            deleteButton.IsEnabled = true;
        }

        
    }
}