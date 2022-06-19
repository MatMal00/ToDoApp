using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class ToDoTaskViewModel: ToDoTaskModel
    {
        public string CategoryName { get; set; }

        public bool IsChecked { get; set; } = false;
    }
}
