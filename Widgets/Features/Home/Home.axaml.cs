using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using widgets.Features.Timer;
using widgets.Features.Widget.Components;
using widgets.Features.Widget.Windows;

namespace widgets.Features.Home;

public partial class HomeWindow : Window
{
    private readonly List<WidgetPanelControl> _widgets = [];

    public HomeWindow()
    {
        InitializeComponent();

        Width = 250;
        SizeToContent = SizeToContent.Height;
        SystemDecorations = SystemDecorations.BorderOnly;

        this.ApplyDefaultWindowProperties(prop =>
        {
            prop.ExtendClientAreaToDecorationsHint = true;
            prop.Transparent = false;
        });

        _widgets.Add(new WidgetPanelControl(new WidgetPanelControlParams("Timer", CreateTimerWindow)));
        _widgets.Add(new WidgetPanelControl(new WidgetPanelControlParams("Webview", CreateWebviewWindow)));

        UpdateWidgetsList();
    }

    public void UpdateWidgetsList()
    {
        WidgetsPanel.Children.Clear();
        WidgetsPanel.Children.AddRange(_widgets);
    }

    private static async Task<bool> IsServerRunningAsync(string url)
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(2);
            var response = await client.GetAsync(url);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private void StartLocalSampleWebviewServer()
    {
        try
        {
            Cursor = new Cursor(StandardCursorType.Wait);

            var workingDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"../../../../Widgets.SampleWebView"));

            // Step 1: Run `npm install`
            var installProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/c npm install",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = workingDir,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            installProcess.Start();
            installProcess.WaitForExit();

            if (installProcess.ExitCode != 0)
                throw new Exception("npm install failed.");

            // Step 2: Run `npm start`
            var startProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/c npm run start",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = workingDir,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            startProcess.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start web server: {ex.Message}");
            return;
        }
        finally
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
        }
    }

    private async void CreateWebviewWindow(object? sender, RoutedEventArgs args)
    {

        var url = "http://127.0.0.1:5500/";
        var isServerRunning = await IsServerRunningAsync(url);

        if (!isServerRunning)
            StartLocalSampleWebviewServer();

        var widgetWindow = new WidgetWindow(url) { ShowInTaskbar = false };
        widgetWindow.Show(this);
    }

    private void CreateTimerWindow(object? sender, RoutedEventArgs args)
    {
        var timerWindow = new TimerWindow();
        timerWindow.Show(this);
    }

    public void PositionWindow()
    {
        var screenSize = Screens.Primary!.WorkingArea.Size;
        var windowSize = PixelSize.FromSize(ClientSize, Screens.Primary.Scaling);
        Position = new PixelPoint(
            screenSize.Width - windowSize.Width - 3,
            screenSize.Height - windowSize.Height - 3);
    }
}