using System.Collections.Generic;
using System.IO;

namespace Outliers
{
    //Interface for outputting list of data points to different channels e.g. CSV file, database
    public interface IDataOutput
    {
        void OutputData(List<DataPoint> dataSet);
    }

    public class CSVDataOutput : IDataOutput
    {

        private readonly string filePath;

        public CSVDataOutput(string filePath)
        {
            this.filePath = filePath;
        }

        public void OutputData(List<DataPoint> dataSet)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine("Date,Price");
                foreach (DataPoint dataPoint in dataSet)
                {
                    writer.WriteLine(string.Format("{0},{1}", dataPoint.DataDateTime.ToString("dd/MM/yyyy"), dataPoint.Price.ToString()));
                }
            }
        }
    }
}