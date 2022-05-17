using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ToDoApp
{
    public class TasksToDoViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoTaskViewModel> ToDoTasksList { get; set; } = new ObservableCollection<ToDoTaskViewModel>();

        public string NewWorkTaskDescription { get; set; }

        public string NewWorkTaskCategory { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
        }

        private void AddNewTask()
        {
            var newTask = new ToDoTaskViewModel()
            {
                Description = NewWorkTaskDescription,
                CreationDate = DateTime.Now,
                CategoryType = "Shopping",
            };

            ToDoTasksList.Add(newTask);
            MessageBox.Show("dsds");
            NewWorkTaskDescription = String.Empty;
        }

    }
}
