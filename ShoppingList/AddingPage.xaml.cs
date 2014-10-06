using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ShoppingList.Logic;

namespace ShoppingList
{
    public partial class AddingPage : PhoneApplicationPage
    {

        private ShoppingItem item;
        private Boolean editMode;

        IEnumerable<Category> categories = Enum.GetValues(typeof(Category)).Cast<Category>();

        public AddingPage()
        {
            InitializeComponent();
            categoriesListPicker.ItemsSource = categories;
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            if (ItemName.Text != "" && ItemAmount.Text != "")
            {
                item.Name = ItemName.Text;
                item.Amount = Double.Parse(ItemAmount.Text);
                item.Category = (Category)categoriesListPicker.SelectedItem;
                if (!editMode) MainPage.shoppingList.Add(item);

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("You didn't put some of the information.");
            }
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.NavigationContext.QueryString.ContainsKey("editmode"))
            {
                editMode = Boolean.Parse(this.NavigationContext.QueryString["editmode"]);
                if (editMode)
                {
                    item = PhoneApplicationService.Current.State["EditedItem"] as ShoppingItem;
                    setUpView(item);
                }
            }
            else
            {
                item = new ShoppingItem();
                editMode = false;
            }
        }

        private void setUpView(ShoppingItem item)
        {
            ItemName.DataContext = item;
            ItemAmount.DataContext = item;
            categoriesListPicker.DataContext = item;
        }

        private int getIndexFromCategory(Category category)
        {
            int index = 0;
            foreach (Category cat in categories)
            {
                if (cat.Equals(category)) return index;
                index++;
            }
            return -1;
        }

    }
}