using System;
using System.Windows;
using System.Windows.Input;

namespace Student_ERP
{
    public class CommandsProject
    {
        public static RoutedCommand CloseCommand;
        public static RoutedCommand EditCommand;
        public static RoutedCommand AddCommand;
        public static RoutedCommand RemoveCommand;
        public static RoutedCommand AboutCommand;
        static CommandsProject()
        {
            CloseCommand = new RoutedCommand("Exit", typeof(MainWindow));
            EditCommand = new RoutedCommand("Edit",typeof(MainWindow));
            AddCommand = new RoutedCommand("Add", typeof(MainWindow));
            RemoveCommand = new RoutedCommand("Remove",typeof(MainWindow));
            AboutCommand = new RoutedCommand("About",typeof(MainWindow));
        }

    }
}