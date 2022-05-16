using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class BaseTask: BaseViewModel
    {
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string CategoryType { get; set; } = CatoegoryTypes.Work;
    }
}
