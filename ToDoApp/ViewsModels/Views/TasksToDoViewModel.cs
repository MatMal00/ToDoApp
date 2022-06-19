using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ToDoApp
{
    public class TasksToDoViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoTaskModel> ToDoTasksList { get; set; } = new ObservableCollection<ToDoTaskModel>();

        public ICommand AddNewTaskCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            GetTasks();
        }

        private void AddNewTask()
        {
            AddTaskModalView addTaskModal = new AddTaskModalView();
            addTaskModal.ShowDialog();

            if (addTaskModal.isAdding == true)
            {
                var newTask = new ToDoTaskModel()
                {
                    Title = addTaskModal.taskTitle,
                    Description = addTaskModal.taskDescription,
                    CategoryId = 1,
                    CreationDate = DateTime.Now,
                };

                using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
                {
                    db.ToDo.Add(newTask);
                    db.SaveChanges();
                }

                ToDoTasksList.Add(newTask);
            }

        }
        public void GetTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                var ToDoTasks = db.ToDo.ToList<ToDoTaskModel>();

                foreach (var item in ToDoTasks)
                    ToDoTasksList.Add(item);
            }
        }


    }
}
