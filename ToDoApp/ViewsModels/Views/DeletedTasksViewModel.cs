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
            RestoreToDoCommand = new RelayCommand(MarkAsDone);
            GetDeletedTasks();
        }

        public void GetDeletedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                DeletedTasksList.Clear();

                var ToDoTasks = db.Done.Join(
                   db.Categories,
                   todo => todo.CategoryId,
                   category => category.Id,
                   (todo, category) => new DeletedTaskViewModel()
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
                    DeletedTasksList.Add(item);
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

                GetDeletedTasks();
            }
        }


        private void DeletePermanentlySelectedTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DeletedTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.ToDo.Find(task.Id));
                }

                db.SaveChanges();
                GetDeletedTasks();
            }
        }

        private void MarkAsDone()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in DeletedTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.ToDo.Find(task.Id));

                    db.Done.Add(new DoneTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                        CompletionDate = new DateTime()
                    });
                }

                db.SaveChanges();
                GetDeletedTasks();
            }
        }
    }
}
