using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ToDoApp
{
    public class DoneTasksViewModel : BaseViewModel
    {
        public ObservableCollection<DoneTaskViewModel> DoneTasksList { get; set; } = new ObservableCollection<DoneTaskViewModel>();

        public ICommand DeleteTaskCommand { get; set; }

        public ICommand RestoreToDoCommand { get; set; }

        public DoneTasksViewModel()
        {
            DeleteTaskCommand = new RelayCommand(DeleteSelectedTasks);
            RestoreToDoCommand = new RelayCommand(RestoreToDo);
            GetDoneTasks();
        }

        public void GetDoneTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                DoneTasksList.Clear();

                var ToDoTasks = db.Done.Join(
                   db.Categories,
                   done => done.CategoryId,
                   category => category.Id,
                   (done, category) => new DoneTaskViewModel()
                   {
                       Id = done.Id,
                       Title = done.Title,
                       Description = done.Description,
                       CreationDate = done.CreationDate,
                       CompletionDate = done.CompletionDate,
                       CategoryId = done.CategoryId,
                       CategoryName = category.Name,
                   }
                   ).ToList();

                foreach (var item in ToDoTasks)
                    DoneTasksList.Add(item);
            }
        }


        private void DeleteSelectedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DoneTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.Done.Find(task.Id));

                    db.Deleted.Add(new DeletedTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                        RemovalDate = DateTime.Now,
                    });
                }

                db.SaveChanges();
                GetDoneTasks();
            }
        }

        private void RestoreToDo()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DoneTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.Done.Find(task.Id));

                    db.ToDo.Add(new ToDoTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                    });
                }

                db.SaveChanges();
                GetDoneTasks();
            }
        }
    }
}
