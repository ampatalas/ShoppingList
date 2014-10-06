using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Logic
{
    public class ShoppingItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private String name;
        private double amount;

        public Category category { get; set; }
        public double Amount {
            get { return amount; }
            set
            {
                if (value == 0) throw new InvalidShoppingItemException("The amount of item cannot be 0.");
                amount = value;
            }
        }

        public String Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new InvalidShoppingItemException("Shopping item name can't be empty.");
                name = value;
                OnPropertyChanged("Name");
            }
        }
        

        public ShoppingItem(String name, Category category, double amount)
        {
            this.Name = name;
            this.category = category;
            this.amount = amount;
        }

        public ShoppingItem() { }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
    }
}
