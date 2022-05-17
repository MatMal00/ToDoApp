using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ToDoApp
{
    public class ToDoTaskViewModel: BaseTask
    {
        public ICommand MarkAsDoneTaskCommand { get; set; }

        public ToDoTaskViewModel()
        {
            MarkAsDoneTaskCommand = new RelayCommand(MarkAsDoneTask);
        }

        private void MarkAsDoneTask()
        {
            MessageBox.Show(TaskId.ToString());
        }
    }
}
