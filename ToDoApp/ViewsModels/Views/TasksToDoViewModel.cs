using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ToDoApp
{
    public class TasksToDoViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoControl> ToDoTasksList { get; set; } = new ObservableCollection<ToDoControl>();

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

                GetTasks();
            }

        }

        public void GetTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                ToDoTasksList.Clear();

                var ToDoTasks = db.ToDo.Join(
                   db.Categories,
                   todo => todo.CategoryId,
                   category => category.Id,
                   (todo, category) => new ToDoControl
                   {
                       Id = todo.Id,
                       Title = todo.Title,
                       Description = todo.Description,
                       CreationDate = todo.CreationDate,
                       CategoryId = todo.CategoryId,
                       CategoryName = category.Name,

                   }
                   ).ToList();

                foreach (var item in ToDoTasks)
                    ToDoTasksList.Add(item);
            }
        }


    }
}
