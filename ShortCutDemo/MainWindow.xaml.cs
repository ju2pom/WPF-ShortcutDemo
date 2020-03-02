using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace ShortCutDemo
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      this.ToggleMaximizeCommand = new RelayCommand(this.ToggleFullscreenExecute);
      this.DataContext = new TextEditorViewModel();
    }

    public ICommand ToggleMaximizeCommand { get; }

    private void ToggleFullscreenExecute()
    {
      if (this.WindowState == WindowState.Maximized)
      {
        this.WindowState = WindowState.Normal;
      }
      else
      {
        this.WindowState = WindowState.Maximized;
      }
    }
  }
}
