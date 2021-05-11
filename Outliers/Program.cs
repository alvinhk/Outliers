using System;
using System.Collections.Generic;

namespace Outliers
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string inputPath = currentFolder + @"\Outliers.csv";
            string outputPath = currentFolder + @"\Outliers(Output).csv";

            var csvDataSource = new CSVDataSource(inputPath);
            List<DataPoint> dataSet = null;
            try
            {
                //read data from CSV file
                dataSet = csvDataSource.GetData();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in reading data");
                Console.WriteLine(e.Message);
            }
            var outlierFound = false;
            List<DataPoint> outputDataSet = null;
            if (dataSet != null)
            {
                outputDataSet = new ZScoreOutlierAnalysis(10.0, 10).RemoveOutliers(dataSet, out outlierFound);
                if (outlierFound)
                {
                    Console.WriteLine("Outlier found");
                    var outputWriter = new CSVDataOutput(outputPath);
                    try
                    {
                        //write data to CSV file
                        outputWriter.OutputData(outputDataSet);
                        Console.WriteLine("Result saved in " + outputPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in writing output file");
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("No outlier");
                }
            }
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
