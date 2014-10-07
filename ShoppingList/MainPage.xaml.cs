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
                }
            }
        }
        private void ClickEdit(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["EditedItem"] = shoppingListBinding.SelectedItem as ShoppingItem;
            this.NavigationService.Navigate(new Uri("/EditItem", UriKind.Relative));
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