using DevExpress.Mvvm.Native;
using DevExpressReportResearching.Infrastructure.Commands;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services.Interfaces;
using DevExpressReportResearching.ViewModels.Base;
using DevExpressReportResearching.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevExpressReportResearching.ViewModels
{
    class ChooseEmployersViewModel : ViewModel
    {
        private ObservableCollection<Employers> _availableEmployers;
        private ObservableCollection<Employers> _selectedEmployers;
        private List<Employers> _selectedAvailableEmployer;

        public ChooseEmployersViewModel(List<Employers> list)
        {
            AvailableEmployers = list.ToObservableCollection();
            SelectedEmployers = new();
            SelectedAvailableEmployers = new();
            AddEmployerCommand = new RelayCommand(AddEmployer, CanAddEmployer);
            RemoveEmployerCommand = new RelayCommand(RemoveEmployer, CanRemoveEmployer);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        public ObservableCollection<Employers> AvailableEmployers
        {
            get => _availableEmployers;
            set => Set(ref _availableEmployers, value);
        }

        public ObservableCollection<Employers> SelectedEmployers
        {
            get => _selectedEmployers;
            set => Set(ref _selectedEmployers, value);
        }

        public List<Employers> SelectedAvailableEmployers
        {
            get => _selectedAvailableEmployer;
            set
            {
                Set(ref _selectedAvailableEmployer, value);
                AddEmployer();
            }
        }

        public Employers SelectedSelectedEmployer { get; set; }

        public ICommand AddEmployerCommand { get; }
        public ICommand RemoveEmployerCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private bool CanAddEmployer() => SelectedAvailableEmployers != null && SelectedAvailableEmployers.Count > 0;
        private void AddEmployer()
        {
            foreach (var employer in SelectedAvailableEmployers.Where(e => !SelectedEmployers.Contains(e)).ToList())
            {
                SelectedEmployers.Add(employer);
            }
        }

        private bool CanRemoveEmployer() => SelectedSelectedEmployer != null;
        private void RemoveEmployer()
        {
            if (SelectedSelectedEmployer != null)
            {
                SelectedEmployers.Remove(SelectedSelectedEmployer);
            }
        }

        private void Confirm()
        {
            ChooseReportWindow chooseReportWindow = new ChooseReportWindow();
            chooseReportWindow.DataContext = new ChooseReportViewModel(SelectedEmployers.ToList());
            chooseReportWindow.Owner = App.ActivedWindow;
            chooseReportWindow.ShowDialog();
            var myWindow = App.Current.Windows
                .OfType<ChooseEmployersWindow>()
    .           FirstOrDefault();
            myWindow.Close();
            
        }

        private void Cancel()
        {
            App.ActivedWindow.Close();
        }
    }
}
