using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
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
            ToolTipText = EdgeApp.Name,
            ContextMenu = new ContextMenu
            {
                Items = { new MenuItem
                {
                    Header = "切换用户"
                }, new MenuItem { Header = "关闭" }, }
            },
        };
        tbi.DoubleClickCommand = new SetVisibleCommand();
    }
}

internal class SetVisibleCommand : ICommand
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

internal class SwitchSignInCommand : ICommand
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