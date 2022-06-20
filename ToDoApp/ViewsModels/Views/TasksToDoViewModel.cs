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
        public ObservableCollection<ToDoTaskViewModel> ToDoTasksList { get; set; } = new ObservableCollection<ToDoTaskViewModel>();

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteTaskCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteTaskCommand = new RelayCommand(DeleteSelectedTasks);
            GetTasks();
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
                   (todo, category) => new ToDoTaskViewModel
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
                    CategoryId = addTaskModal.taskCategoryId,
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

        private void DeleteSelectedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in ToDoTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.ToDo.Find(task.Id));

                    db.Deleted.Add(new DeletedTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                        RemovalDate = new DateTime()
                    });
                }

                db.SaveChanges();
                GetTasks();
            }
        }
    }
}
