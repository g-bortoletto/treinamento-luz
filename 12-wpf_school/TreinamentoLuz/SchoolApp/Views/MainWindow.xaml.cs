using SchoolApp.ViewModels;
using System.Windows;

namespace SchoolApp.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel vm;

		public MainWindow()
		{
			InitializeComponent();
			
			vm = new MainViewModel();
			DataContext = vm;
		}
	}
}
