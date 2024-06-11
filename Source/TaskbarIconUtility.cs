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
        var tbi = new TaskbarIcon()
        {
            Icon = new Icon("./static/favicon.ico"),
            ToolTipText = EdgeApplication.Name
        };
        tbi.DoubleClickCommand = new ToggleCommand();
    }
}

internal class ToggleCommand : ICommand
{
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        // EdgeApplication.Win.Visibility = Visibility.Visible;
        // EdgeApplication.Win.Activate();
    }

    public event EventHandler CanExecuteChanged;
}