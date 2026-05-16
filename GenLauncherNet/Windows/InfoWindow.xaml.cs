using System.Windows;

namespace GenLauncherNet
{
    public partial class InfoWindow : Window
    {
        private bool ContinueLaunch = true;
        private bool ChoseAnOption = false;

        public InfoWindow(string mainInfo, string modsInfo)
        {
            InitializeComponent();
            SetColors();

            MainMessage.Text = mainInfo;
            ModsMessage.Text = modsInfo;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
			ChoseAnOption = true;
			this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ContinueLaunch = false;
			ChoseAnOption = true;
			this.Hide();
        }

        public bool GetResult()
        {
            this.Close();
            return ContinueLaunch;
        }

		public bool UserChoseAnOption()
		{
			return ChoseAnOption;
		}

		private void Continue_Click(object sender, RoutedEventArgs e)
        {
            ChoseAnOption = true;
			this.Hide();
        }

        private void SetColors()
        {
            this.Resources["GenLauncherBorderColor"] = EntryPoint.Colors.GenLauncherBorderColor;
            this.Resources["GenLauncherActiveColor"] = EntryPoint.Colors.GenLauncherActiveColor;
            this.Resources["GenLauncherDarkFillColor"] = EntryPoint.Colors.GenLauncherDarkFillColor;
            this.Resources["GenLauncherInactiveBorder"] = EntryPoint.Colors.GenLauncherInactiveBorder;
            this.Resources["GenLauncherInactiveBorder2"] = EntryPoint.Colors.GenLauncherInactiveBorder2;
            this.Resources["GenLauncherDefaultTextColor"] = EntryPoint.Colors.GenLauncherDefaultTextColor;
            this.Resources["GenLauncherLightBackGround"] = EntryPoint.Colors.GenLauncherLightBackGround;
            this.Resources["GenLauncherDarkBackGround"] = EntryPoint.Colors.GenLauncherDarkBackGround;
            this.Resources["GenLauncherDefaultTextColor"] = EntryPoint.Colors.GenLauncherDefaultTextColor;

            this.Resources["GenLauncherListBoxSelectionColor2"] = EntryPoint.Colors.GenLauncherListBoxSelectionColor2;
            this.Resources["GenLauncherListBoxSelectionColor1"] = EntryPoint.Colors.GenLauncherListBoxSelectionColor1;
            this.Resources["GenLauncherButtonSelectionColor"] = EntryPoint.Colors.GenLauncherButtonSelectionColor;
        }
    }
}
