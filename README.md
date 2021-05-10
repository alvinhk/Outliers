# Outliers

This program removes outliers in time series data by removing data point with high z-score. Z-score is the number of standard deviations by which the value of a data point is above or below the mean value. The algo requires two parameters, threshold and window. If the z-score of the data point is larger than threshold, the data point is removed. Window is the number of historical data points that are used to calculate mean and standard deviation.

## Running the program

The program is developed using .NET Core 3.1. IDE is Visual Studio Code. It is a console application. It reads a CSV file with file name “Outliers.csv” in the same folder as the EXE file. It will output the result CSV file with file name “Outliers(Output).csv” in that folder. If there is no outlier found, it will show “No outlier” in the console instead of writing the CSV file. 

Build and run the code:
In terminal, go to Outlier folder and run “dotnet run” 

Run the program only:
Go to folder Outlier\bin\MCD\Debug\netcoreapp3.1\publish and run Outliers.exe
