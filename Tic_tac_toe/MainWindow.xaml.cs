using System.Windows;
using Tic_tac_toe.ViewModels;

namespace Tic_tac_toe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}