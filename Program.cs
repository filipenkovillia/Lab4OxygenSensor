using System;
using System.Threading;

namespace Lab4OxygenSensor
{
    class Program
    {
        private const double k = 20.95;
        private const int O2SensorPeriod = 14;
        private const int CO2SensorPeriod = 20;
        private const int MaxWorkTime = 100;

        static void Main(string[] args)
        {
            double[] O2Values = new double[MaxWorkTime];
            double[] CO2Values = new double[MaxWorkTime];
            Random rand = new Random();
            for (int i = 0; i < MaxWorkTime; i++)
            {
                O2Values[i] = rand.Next(1001) * 0.1;
                CO2Values[i] = rand.Next(1001) * 0.1;
            }

            int time = 0;
            double O2currentValue = 1;
            double CO2currentValue = 1;
            while (time < MaxWorkTime)
            {
                if (time % O2SensorPeriod == 0 || time % CO2SensorPeriod == 0)
                {
                    if (time % O2SensorPeriod == 0)
                        O2currentValue = O2Values[time];

                    if (time % CO2SensorPeriod == 0)
                        CO2currentValue = CO2Values[time];

                    double O2Concentration = Concentration(O2currentValue);
                    double CO2Concentration = Concentration(CO2currentValue);

                    Console.Clear();
                    Console.WriteLine($"O2 concentration: {Math.Round(O2Concentration, 3)}");
                    Console.WriteLine($"CO2 concentration: {Math.Round(CO2Concentration, 3)}");
                    Console.WriteLine($"Respiratory rate: {Math.Round(RespiratoryRate(O2Concentration, CO2Concentration), 3)}");
                }

                Console.Write($"\rTime passed: {time} seconds");

                Thread.Sleep(1000);
                time++;
            }
        }

        private static double Concentration(double u)
        {
            return u / k;
        }

        private static double RespiratoryRate(double O2concentration, double CO2concentration)
        {
            return O2concentration / CO2concentration;
        }
    }
}
