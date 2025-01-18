using System.Windows;
using Milek_Nowak_WindowsApp.ViewModels;

namespace Milek_Nowak_WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            DataContext = new
            {
                BroomListViewModel = new BroomListViewModel(),
                ProducerListViewModel = new ProducerListViewModel()
            };
            */
            DataContext = new ListsViewModel();
        }
    }
}