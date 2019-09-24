using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                UpdateTotal();
            }
        }

        private int nightsCount;
        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                UpdateTotal();
            }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set
            {
                if (value > 100)
                    throw new ArgumentException();
                discount = value;
                Notify(nameof(Discount));
                UpdateTotal();
            }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                discount = 100 - (100 * total / (price * nightsCount));
                Notify(nameof(Discount));
            }
        }

        private void UpdateTotal()
        {
            total = price * nightsCount * (1 - discount / 100);
            Notify(nameof(Total));
        }
    }
}
