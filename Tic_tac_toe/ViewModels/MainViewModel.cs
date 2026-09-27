using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tic_tac_toe.Models;

namespace Tic_tac_toe.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly TicTacToeGame game = new();

        public ObservableCollection<CellViewModel> Cells { get; } = new();

        private int level = 1;
        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }

        private string message = "Ваш хід";
        public string Message
        {
            get => message;
            private set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand MoveCommand { get; }
        public RelayCommand NewGameCommand { get; }

        public MainViewModel()
        {
            MoveCommand = new RelayCommand(parameter =>
            {
                if (parameter is CellViewModel cell)
                    MakeMove(cell);
            });

            NewGameCommand = new RelayCommand(_ => NewGame());

            NewGame();
        }

        private void NewGame()
        {
            game.NewGame();
            Cells.Clear();

            for (int row = 0; row < TicTacToeGame.Size; row++)
                for (int col = 0; col < TicTacToeGame.Size; col++)
                    Cells.Add(new CellViewModel(row, col));

            Message = "Ваш хід";
        }

        private void MakeMove(CellViewModel cell)
        {
            if (!game.MakeMove(cell.Row, cell.Column, TicTacToeGame.Human))
                return;

            cell.Value = "X";

            if (game.GameOver)
            {
                Message = game.CheckWinner(TicTacToeGame.Human)
                    ? "Ви перемогли!"
                    : "Нічия!";
                return;
            }

            var move = game.ChooseComputerMove(Level);

            if (move == null)
                return;

            game.MakeMove(move.Value.row, move.Value.col, TicTacToeGame.Computer);

            Cells[move.Value.row * TicTacToeGame.Size + move.Value.col].Value = "O";

            Message = game.CheckWinner(TicTacToeGame.Computer)
                ? "Комп'ютер переміг!"
                : game.IsDraw() ? "Нічия!" : "Ваш хід";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}