using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.ViewsModels.Views
{
    internal class AddTaskModalViewModel: BaseViewModel
    {
        public string NewWorkTaskTitle { get; set; }

        public string NewWorkTaskDescription { get; set; }
    }
}
