using BHBackup.Common;
using BHBackup.Engine;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace BHBackup.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        this.OutputDirectory.Text = Path.Join(
            Environment.GetFolderPath(
                Environment.SpecialFolder.Personal
            ),
            $"BHBackup_{DateTime.Now:yyyy_MM_dd}"
        );
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo(
                e.Uri.ToString()
            )
            {
                UseShellExecute = true
            }
        );
    }

    private async void StartBackup_Click(object sender, RoutedEventArgs e)
    {
        if (!this.ValidateForm())
        {
            return;
        }

        var progress = new BackupProgress();

        var logger = new EventingLogger();
        logger.EventLogged += (LogLevel logLevel, EventId eventId, Exception? exception, string message) =>
            Application.Current.Dispatcher.Invoke(
                () => progress.Status.Text = message
            );

        var backupOptions = new BackupOptions
        {
            Logger = logger,
            Username = this.Username.Text,
            Password = this.Password.Password,
            OutputDirectory = this.OutputDirectory.Text,
            SkipDownload = false,
            SkipGenerate = false,
            SkipLaunch = false
        };

        // show the progress form modally but allow the main backup to run async
        // see https://stackoverflow.com/questions/33406939/async-showdialog
        await Task.Run(
            new Action(() =>
                {
                    this.Dispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            progress.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            progress.Owner = this;
                            progress.Status.Text = "Starting backup...";
                            progress.ShowDialog();
                        })
                    );
                    this.Dispatcher.Invoke(() =>
                        {
                            progress.Top = this.Top + (this.Height - progress.Height) / 2;
                            progress.Left = this.Left + (this.Width - progress.Width) / 2;
                        }
                    );
                    try
                    {
                        BackupEngine.ExecuteBackup(backupOptions).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessageBox("An error occurred", ex.Message);
                    }
                }
            )
        ).ContinueWith(
            new Action<Task>(task =>
            {
                progress.Close();
            }
        ), TaskScheduler.FromCurrentSynchronizationContext());

    }

    private bool ValidateForm()
    {
        if (string.IsNullOrEmpty(this.Username.Text))
        {
            this.ShowMessageBox("An error occurred", "Please enter a username");
            this.Username.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(this.Password.Password))
        {
            this.ShowMessageBox("An error occurred", "Please enter a password");
            this.Password.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(this.OutputDirectory.Text))
        {
            this.ShowMessageBox("An error occurred", "Please enter an output folder");
            this.OutputDirectory.Focus();
            return false;
        }
        return true;
    }

    private void ShowMessageBox(string title, string message)
    {
        this.Dispatcher.Invoke(() =>
            {
                var msgbox = new MessageBox();
                this.Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        msgbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        msgbox.Owner = this;
                        msgbox.ShowDialog();
                    })
                );
                msgbox.Top = this.Top + (this.Height - msgbox.Height) / 2;
                msgbox.Left = this.Left + (this.Width - msgbox.Width) / 2;
                msgbox.Title = "BHBackup";
                msgbox.MsgBoxTitle.Content = title;
                msgbox.MsgBoxMessage.Text = message;
            }
        );
    }

}
