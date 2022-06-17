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
        static readonly string connectionString = @"Data Source=DESKTOP-SO4MQ1P;Initial Catalog=todoApp;Integrated Security=True";

        public ObservableCollection<ToDoTaskModel> ToDoTasksList { get; set; } = new ObservableCollection<ToDoTaskModel>();

        public ICommand AddNewTaskCommand { get; set; }

        public TasksToDoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            GetTasks().Wait();
        }

        private void AddNewTask()
        {
            AddTaskModalView addTaskModal = new AddTaskModalView();
            addTaskModal.ShowDialog();

            var newTask = new ToDoTaskModel()
            {
                Title = addTaskModal.taskTitle,
                Description = addTaskModal.taskDescription,
                CategoryId = 1,
                CreationDate = DateTime.Now,
             };

            using (ToDoAppContext db = new ToDoAppContext(connectionString))
            {
                db.ToDo.Add(newTask);
                db.SaveChanges();
            }

            ToDoTasksList.Add(newTask);
        }
        public async Task GetTasks()
        {
            using (ToDoAppContext db = new ToDoAppContext(connectionString))
            {
                var ToDoTasks = db.ToDo.ToList<ToDoTaskModel>();
                ToDoTasksList.ToList().AddRange(ToDoTasks);
               
            }
        }


    }
}
