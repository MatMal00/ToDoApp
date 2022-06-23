using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ToDoApp
{
    public class DeletedTasksViewModel : BaseViewModel
    {
        public ObservableCollection<DeletedTaskViewModel> DeletedTasksList { get; set; } = new ObservableCollection<DeletedTaskViewModel>();

        public ICommand DeleteTaskPernamentlyCommand { get; set; }

        public ICommand RestoreToDoCommand { get; set; }

        public DeletedTasksViewModel()
        {
            DeleteTaskPernamentlyCommand = new RelayCommand(DeletePermanentlySelectedTasks);
            RestoreToDoCommand = new RelayCommand(RestoreToDo);
            GetDeletedTasks();
        }

        public void GetDeletedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                DeletedTasksList.Clear();

                var ToDoTasks = db.Deleted.Join(
                   db.Categories,
                   todo => todo.CategoryId,
                   category => category.Id,
                   (todo, category) => new DeletedTaskViewModel()
                   {
                       Id = todo.Id,
                       Title = todo.Title,
                       Description = todo.Description,
                       CreationDate = todo.CreationDate,
                       RemovalDate = todo.RemovalDate,
                       CategoryId = todo.CategoryId,
                       CategoryName = category.Name,
                   }
                   ).ToList();

                foreach (var item in ToDoTasks)
                    DeletedTasksList.Add(item);
            }
        }


        private void DeletePermanentlySelectedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DeletedTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.Deleted.Find(task.Id));
                }

                db.SaveChanges();
                GetDeletedTasks();
            }
        }

        private void RestoreToDo()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DeletedTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.Deleted.Find(task.Id));

                    db.ToDo.Add(new ToDoTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                    });
                }

                db.SaveChanges();
                GetDeletedTasks();
            }
        }
    }
}
