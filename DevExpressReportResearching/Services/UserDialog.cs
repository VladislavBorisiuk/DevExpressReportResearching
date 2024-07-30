using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services.Interfaces;
using DevExpressReportResearching.ViewModels;
using DevExpressReportResearching.Views.Windows;
using System.Windows;


namespace DevExpressReportResearching.Services
{
    internal class UserDialog : IUserDialog
    {
        public bool Confirm(string Message, string Caption, bool Exclamation = false)=>
            System.Windows.MessageBox.Show(
            Message,
            Caption,
            MessageBoxButton.YesNo,
            Exclamation ? MessageBoxImage.Exclamation : MessageBoxImage.Exclamation) 
                == MessageBoxResult.Yes;
        


        public void Choose(object? item)
        {
            switch (item)
            {
                case null:
                    Console.WriteLine("Объект является null.");
                    break;
                case List<Employers> employersList:
                    ChooseEmployersWindow chooseEmployersWindow = new ChooseEmployersWindow();
                    chooseEmployersWindow.DataContext = new ChooseEmployersViewModel(employersList);
                    chooseEmployersWindow.Owner = App.Current.MainWindow; 
                    chooseEmployersWindow.ShowDialog(); 
                    break;
                default:
                    Console.WriteLine($"Объекты типа {item.GetType().Name} не поддерживаются.");
                    break;
            }
        }

        public void ShowError(string Message, string Caption)
        {
            throw new NotImplementedException();
        }

        public void ShowInformation(string Message, string Caption)
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(string Message, string Caption)
        {
            throw new NotImplementedException();
        }
    }
}
