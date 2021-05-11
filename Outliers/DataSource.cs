using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Outliers
{
    //Interface for reading data from different channels e.g. CSV file, database and convert to list of data point
    public interface IDataSource
    {
        List<DataPoint> GetData();
    }

    public class CSVDataSource : IDataSource
    {

        private readonly string filePath;

        public CSVDataSource(string filePath)
        {
            this.filePath = filePath;
        }

        public List<DataPoint> GetData()
        {
            var dataSet = new List<DataPoint>();
            var reader = new StreamReader(File.OpenRead(filePath));
            do
            {
                var line = reader.ReadLine();
                var lineSplit = line.Split(',');
                if (lineSplit.Length >= 2)
                {
                    DateTime dateTime;
                    double price;
                    if (DateTime.TryParseExact(lineSplit[0], "dd/MM/yyyy", null, DateTimeStyles.None, out dateTime) && double.TryParse(lineSplit[1], out price))
                    {
                        dataSet.Add(new DataPoint(dateTime, price));
                    }
                }
            } while (!reader.EndOfStream);
            reader.Close();
            return dataSet;
        }
    }
}