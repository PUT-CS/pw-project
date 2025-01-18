using System.Windows;
using Barski_Lewandowski_WindowsApp.ViewModels;

namespace Barski_Lewandowski_WindowsApp
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