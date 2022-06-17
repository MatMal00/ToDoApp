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
using System.Windows.Shapes;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for AddTaskModalView.xaml
    /// </summary>
    public partial class AddTaskModalView : Window
    {
        public string taskTitle { get; set; }

        public string taskDescription { get; set; }

        public string taskCategory { get; set; }

        public AddTaskModalView()
        {
            InitializeComponent();
        }

        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            taskTitle = titleTextBox.Text;
            taskDescription = descriptionTextBox.Text;
            taskCategory = "Udało się";
            this.Close();
        }
    }
}
