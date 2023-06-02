using System;
using System.Windows;
using System.Windows.Controls;

namespace DiskBenchmark.Infrastructure.Commands
{
    internal class SetLanguageCommand : Command
    {
        private static string CurrentLanguage = "en";
        public override bool CanExecute(object parameter)
        {
            if (!(parameter is ComboBoxItem comboBoxItem)) return false;
            if (comboBoxItem.Name.ToString() != CurrentLanguage)
            {
                CurrentLanguage = comboBoxItem.Name.ToString();
                return true;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            if (!(parameter is ComboBoxItem comboBoxItem)) return;
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (comboBoxItem.Name.ToString())
            {
                case "ua":
                    dictionary.Source = new Uri(@"..\Resources\Langs\ua.xaml",UriKind.Relative);                   
                    break;
                case "en":
                    dictionary.Source = new Uri(@"..\Resources\Langs\en-US.xaml", UriKind.Relative);                   
                    break;
                default:
                    dictionary.Source = new Uri(@"..\Resources\Langs\en-US.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
