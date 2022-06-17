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

        public ICommand AddNewTaskCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
        }

        private void AddNewTask()
        {
            AddTaskModalView addTaskModal = new AddTaskModalView();
            addTaskModal.ShowDialog();

            var newTask = new ToDoTaskViewModel()
            {
                Title = addTaskModal.taskTitle,
                Description = addTaskModal.taskDescription,
                CreationDate = DateTime.Now,
                CategoryType = addTaskModal.taskCategory,
             };

            ToDoTasksList.Add(newTask);
        }

    }
}
