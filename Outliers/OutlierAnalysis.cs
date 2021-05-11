using System;
using System.Collections.Generic;
using System.Linq;

namespace Outliers
{
    //Interface for outlier analysis. Different outlier detection algo can implement this interface.
    public interface IOutlierAnalysis
    {
        List<DataPoint> RemoveOutliers(List<DataPoint> dataSet, out bool outlierFound);
    }

    //remove outlier by removing data point with high z-score
    //z-score is the number of standard deviations by which the value of a data point is above or below the mean value.
    public class ZScoreOutlierAnalysis : IOutlierAnalysis
    {

        private readonly double threshold;
        private readonly int window;

        //if the z-score of the data point is larger than threshold, the data point is removed.
        //window is the number of historical data points that are used to calculate mean and standard deviation.
        public ZScoreOutlierAnalysis(double threshold, int window)
        {
            this.threshold = threshold;
            this.window = window;
        }

        private bool CheckOutlier(List<DataPoint> dataSet, DataPoint newDataPoint)
        {
            if (dataSet.Count == 1)
            {
                return false;
            }
            var filterDataSet = dataSet.Skip(Math.Max(0, dataSet.Count - window));
            double sum = 0.00D, mean = 0.00D;
            double bigSum = 0.00, stdDev;
            foreach (DataPoint dataPoint in filterDataSet)
            {
                sum += dataPoint.Price;
            }
            mean = sum / filterDataSet.Count();
            foreach (DataPoint dataPoint in filterDataSet)
            {
                bigSum += Math.Pow(dataPoint.Price - mean, 2);
            }
            stdDev = Math.Sqrt(bigSum / (filterDataSet.Count() - 1));
            var zScore = (newDataPoint.Price - mean) / stdDev;
            if (zScore > threshold || zScore < threshold * -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DataPoint> RemoveOutliers(List<DataPoint> dataSet, out bool outlierFound)
        {
            outlierFound = false;
            var outputArray = new List<DataPoint>();
            for (int i = 0; i < dataSet.Count; i++)
            {
                if (i == 0)
                {
                    outputArray.Add(dataSet[0]);
                }
                else
                {
                    if (!CheckOutlier(dataSet.Take(i).ToList(), (DataPoint)dataSet[i]))
                    {
                        outputArray.Add(dataSet[i]);
                    }
                    else
                    {
                        outlierFound = true;
                    }
                }
            }
            return outputArray;
        }
    }
}