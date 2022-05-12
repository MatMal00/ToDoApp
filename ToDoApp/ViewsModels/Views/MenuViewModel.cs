using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDoApp
{
    public class MenuViewModel
    {
        public ICommand AddNewTaskCommand { get; set; }

        public MenuViewModel()
        {
            AddNewTaskCommand = new RelayCommand(HandlePageChange);
        }

        private void HandlePageChange()
        {
            MessageBox.Show("dsadsa");
        }


    }
}
