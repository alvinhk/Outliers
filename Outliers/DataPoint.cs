using System;

namespace Outliers
{
    //DataPoint is the data related to a timestamp. More data fields can be added such as bid price and ask price
    public class DataPoint
    {
        public DateTime DataDateTime { get; set; }

        public double Price { get; set; }

        public DataPoint(DateTime dataDateTime, double price)
        {
            this.DataDateTime = dataDateTime;
            this.Price = price;
        }
    }
}