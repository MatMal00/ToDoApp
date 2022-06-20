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
    /// Interaction logic for EditTaskModalView.xaml
    /// </summary>
    public partial class EditTaskModalView : Window
    {
        public string taskTitle { get; set; }

        public string taskDescription { get; set; }

        public int taskCategoryId { get; set; }

        public string selectedCategoryName { get; set; }

        public bool isEditing { get; set; } = false;

        public EditTaskModalView(string title, string description, string categoryName)
        {
            InitializeComponent();
            titleTextBox.Text = title;
            descriptionTextBox.Text = description;
            selectedCategoryName = categoryName;    
        }

        private void Button_Edit_Task(object sender, RoutedEventArgs e)
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                taskTitle = titleTextBox.Text;
                taskDescription = descriptionTextBox.Text;
                taskCategoryId = db.Categories.Where(c => c.Name == selectedCategoryName).Single().Id;
                isEditing = true;
                this.Close();
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                var categories = db.Categories.ToList();
                var combo = sender as ComboBox;
                int categoryIndex = 0;

                for (int i = 0; i < categories.Count(); i++)
                {
                    combo.Items.Add(categories[i].Name);
                    if (categories[i].Name == selectedCategoryName) categoryIndex = i;
                }

                combo.SelectedIndex = categoryIndex;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedComboItem = sender as ComboBox;
            selectedCategoryName = selectedComboItem.SelectedItem as string;
        }
    }
}
