using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Widgets.ViewModels;

namespace Widgets;

public class ViewLocator : IDataTemplate
{
    private static Dictionary<Type, Func<Control>> templates = new();

    private static void Register<TViewModel, TView>() 
        where TView : Control, new()
    {
        templates[typeof(TViewModel)] = () => new TView();
    }

    static ViewLocator()
    {
        // Register<SomethingViewModel, SomethingView>();
    }

    public Control? Build(object? param)
    {
        if (param is null)
            return null;
        
        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        
        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
