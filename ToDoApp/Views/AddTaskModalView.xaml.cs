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

        public int taskCategoryId { get; set; }

        public string selectedCategoryName { get; set; }    

        public bool isAdding { get; set; } = false;

        public AddTaskModalView()
        {
            InitializeComponent();
        }

        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                taskTitle = titleTextBox.Text;
                taskDescription = descriptionTextBox.Text;
                taskCategoryId = db.Categories.Where(c => c.Name == selectedCategoryName).Single().Id;
                isAdding = true;
                this.Close();
            }      
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                var categories = db.Categories.ToList();

                var combo = sender as ComboBox;
                foreach (var item in categories)
                    combo.Items.Add(item.Name);

                combo.SelectedIndex = 0;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedComboItem = sender as ComboBox;
            selectedCategoryName = selectedComboItem.SelectedItem as string;
        }
    }
}
