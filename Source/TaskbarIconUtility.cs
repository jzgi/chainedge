using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;

namespace ChainEdge;

public class TaskbarIconUtility
{
    public static void Do()
    {
        //Note: XAML is suggested for all but the simplest scenarios
        TaskbarIcon tbi = new TaskbarIcon()
        {
            Icon = new Icon("./static/favicon.ico"),
            ToolTipText = EdgeApp.Name
        };
        tbi.DoubleClickCommand = new Abc();
    }

    static void Adb()
    {
        
    }
}

class Abc : ICommand
{
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        EdgeApp.Win.Visibility = Visibility.Visible;
        EdgeApp.Win.Activate();
    }

    public event EventHandler CanExecuteChanged;
}