﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for DoneTasksView.xaml
    /// </summary>
    public partial class TasksToDoView : Page
    {
        public TasksToDoView()
        {
            InitializeComponent();
            DataContext = new TasksToDoViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTaskModalView addTaskModalView = new AddTaskModalView();
            addTaskModalView.ShowDialog();  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
