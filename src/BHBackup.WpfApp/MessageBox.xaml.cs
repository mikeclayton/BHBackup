using System.Windows;

namespace BHBackup.WpfApp;

/// <summary>
/// Interaction logic for MessageBox.xaml
/// </summary>
public sealed partial class MessageBox : Window
{

    public MessageBox()
    {
        InitializeComponent();
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}
