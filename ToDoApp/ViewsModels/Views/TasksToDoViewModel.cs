using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ToDoApp
{
    public class TasksToDoViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoTaskViewModel> ToDoTasksList { get; set; } = new ObservableCollection<ToDoTaskViewModel>();

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand EditTaskCommand { get; set; }

        public ICommand DeleteTaskCommand { get; set; }

        public ICommand MarkAsDoneCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteTaskCommand = new RelayCommand(DeleteSelectedTasks);
            EditTaskCommand = new RelayCommand(EditTask);
            MarkAsDoneCommand = new RelayCommand(MarkAsDone);
            GetTasks();
        }

        /// <summary>
        /// Get all tasks to do
        /// </summary>
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

        /// <summary>
        /// Add new task to the list
        /// </summary>
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
                    CreationDate = DateTime.Now.ToString("g"),
                };

                using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
                {
                    db.ToDo.Add(newTask);
                    db.SaveChanges();
                }

                GetTasks();
            }
        }

        /// <summary>
        /// Edit selected task
        /// </summary>
        private void EditTask()
        {
            var getSelected = ToDoTasksList.Where(t => t.IsChecked).ToList();

            if(getSelected != null && getSelected.Count() == 1)
            {
                var selectedTask = getSelected[0];
                EditTaskModalView editTaskModal = new EditTaskModalView(selectedTask.Title, selectedTask.Description, selectedTask.CategoryName);

                editTaskModal.ShowDialog();

                if (editTaskModal.isEditing == true)
                {
                    using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
                    {
                        var taskToEdit = db.ToDo.Find(selectedTask.Id);

                        taskToEdit.Title = editTaskModal.taskTitle;
                        taskToEdit.Description = editTaskModal.taskDescription;
                        taskToEdit.CategoryId = editTaskModal.taskCategoryId;
                        db.SaveChanges();
                    }
                    GetTasks();
                }
            }else
            {
                MessageBox.Show($"{(getSelected.Count() > 1 ? "You can choose only one task at the same time" : "You have to choose task !")}");
            }  

        }

        /// <summary>
        /// Delete selected task (move to deleted tasks list)
        /// </summary>
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
                        RemovalDate = DateTime.Now.ToString("g"),
                    });
                }

                db.SaveChanges();
                GetTasks();
            }
        }

        /// <summary>
        /// Move task to done tasks list
        /// </summary>
        private void MarkAsDone()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                foreach (var task in ToDoTasksList.Where(t => t.IsChecked))
                {
                    db.Remove(db.ToDo.Find(task.Id));

                    db.Done.Add(new DoneTaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        CategoryId = task.CategoryId,
                        CreationDate = task.CreationDate,
                        CompletionDate = DateTime.Now.ToString("g")
                    });
                }

                db.SaveChanges();
                GetTasks();
            }
        }
    }
}
