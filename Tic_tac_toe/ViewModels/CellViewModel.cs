using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tic_tac_toe.ViewModels
{
    internal class CellViewModel : INotifyPropertyChanged
    {
        private string value = "";

        public int Row { get; }
        public int Column { get; }

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsEnabled => Value == "";

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(name));
        }

        public CellViewModel(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}