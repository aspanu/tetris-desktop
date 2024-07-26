using Microsoft.Maui.Controls;
using Plugin.Maui.KeyListener;

namespace TetrisDesktop
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}