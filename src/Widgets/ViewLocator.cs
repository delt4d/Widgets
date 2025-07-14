using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Widgets.UI.ViewModels;
using Widgets.UI.Views;

namespace Widgets;

public class ViewLocator : IDataTemplate
{
    private static readonly Dictionary<Type, Func<Control>> templates = [];

    private static void Register<TViewModel, TView>() 
        where TView : Control, new()
    {
        templates[typeof(TViewModel)] = () => new TView();
    }

    static ViewLocator()
    {
        Register<WidgetItemViewModel, WidgetItemView>();
        Register<WidgetWindowViewModel, WidgetWindow>();
        Register<TimerWidgetWindowViewModel, TimerWidgetWindow>();
    }

    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        if (templates.TryGetValue(param.GetType(), out var template))
            return template();
            
        return null;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
