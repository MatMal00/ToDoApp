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

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        public Navigation()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Redirect to home page
        /// </summary>

        private void Home_Click(object sender, RoutedEventArgs e)
        {

            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("/Views/MenuView.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        ///  Redirect to TasksToDo page
        /// </summary>
        private void TaskToDo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("/Views/TasksToDoView.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        ///  Redirect to TasksDone page
        /// </summary>
        private void TaskDone_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("/Views/DoneTasksView.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        ///  Redirect to TaskDeleted page
        /// </summary>
        private void TaskDeleted_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("/Views/DeletedTasksView.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
