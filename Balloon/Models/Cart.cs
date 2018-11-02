using System.Collections.Generic;
using System.Linq;

namespace Balloon.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Good good, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Good.GoodId == good.GoodId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Good = good,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Good good)
        {
            lineCollection.RemoveAll(l => l.Good.GoodId == good.GoodId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Good.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Good Good { get; set; }
        public int Quantity { get; set; }
    }
}
