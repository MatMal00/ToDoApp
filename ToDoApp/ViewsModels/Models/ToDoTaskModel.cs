﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class ToDoTaskModel: BaseViewModel
    {
        public int Id { get; set; } 

        public string Title { get; set; }   

        public string Description { get; set; } 

        public int CategoryId { get; set; }

        public string CreationDate { get; set; }  
    }
}
