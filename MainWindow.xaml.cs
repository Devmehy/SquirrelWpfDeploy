using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SquirrelWpfDeploy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       UpdateManager manager;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }
      
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager
                .GitHubUpdateManager(@"https://github.com/meJevin/WPFCoreTest");

            CurrentVersionTextBox.Text = manager.CurrentlyInstalledVersion().ToString();
        }
        private async void b_Check_Click(object sender, RoutedEventArgs e)
        {
            var updateInfo = await manager.CheckForUpdate();

            if (updateInfo.ReleasesToApply.Count > 0)
            {
                b_update.IsEnabled = true;
            }
            else
            {
                b_update.IsEnabled = false;
            }
        }

        private async void b_update_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();

            MessageBox.Show("Updated succesfuly!");
        }

       
    }
}
