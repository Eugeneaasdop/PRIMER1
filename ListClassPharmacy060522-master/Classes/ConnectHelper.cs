using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ListClass.Classes
{/// <summary>
/// вспомогательный класс
/// </summary>
    class ConnectHelper
    {
        public static List<Pharmacy> pharmacies = new List<Pharmacy>();

        public static string fileName;

        public static void ReadListFromFile(string filename)
        {
            try
            {
                StreamReader streamReader = new StreamReader(filename, Encoding.UTF8);
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] items = line.Split(';');
                    Pharmacy pharmacy = new Pharmacy()
                    {
                        NamePreparate = items[0].Trim(),
                        CountPreparate = int.Parse(items[1].Trim()),
                        PricePreparate = double.Parse(items[2].Trim()),
                        MonthPreparate = int.Parse(items[3].Trim())
                    };
                    pharmacies.Add(pharmacy);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Неверный формат данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void SaveListToFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            foreach(Pharmacy ph in pharmacies)
            {
                streamWriter.WriteLine($"{ph.NamePreparate};{ph.CountPreparate};{ph.PricePreparate};{ph.MonthPreparate}");
            }
            streamWriter.Close();
        }
       
}
}
