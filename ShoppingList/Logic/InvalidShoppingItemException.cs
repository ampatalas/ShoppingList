using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Logic
{
    using System;

    public class InvalidShoppingItemException : Exception
    {
        public InvalidShoppingItemException()
        {
        }

        public InvalidShoppingItemException(string message)
            : base(message)
        {
        }

        public InvalidShoppingItemException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
