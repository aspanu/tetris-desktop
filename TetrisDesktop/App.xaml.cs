﻿using Microsoft.Maui.Controls;

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