using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ShortCutDemo
{
  public class TextEditorViewModel : ViewModelBase
  {
    public TextEditorViewModel()
    {
      this.ToggleBoldCommand = new RelayCommand(this.ToggleBoldExecute);
      this.ToggleItalicCommand = new RelayCommand(this.ToggleItalicExecute);
      this.ToggleUnderlinedCommand = new RelayCommand(this.ToggleUnderlinedExecute);
    }

    public TextSelection SelectedText { get; set; }

    public ICommand ToggleBoldCommand { get; }
    public ICommand ToggleItalicCommand { get; }
    public ICommand ToggleUnderlinedCommand { get; }

    private void ToggleBoldExecute()
    {
      if (this.SelectedText != null)
      {
        var weight = (FontWeight)this.SelectedText.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Normal
          ? FontWeights.Bold
          : FontWeights.Normal;
        this.SelectedText.ApplyPropertyValue(Inline.FontWeightProperty, weight);
      }
    }

    private void ToggleItalicExecute()
    {
      if (this.SelectedText != null)
      {
        var style = (FontStyle)this.SelectedText.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Normal
          ? FontStyles.Italic
          : FontStyles.Normal;
        this.SelectedText.ApplyPropertyValue(Inline.FontStyleProperty, style);
      }
    }

    private void ToggleUnderlinedExecute()
    {
      if (this.SelectedText != null)
      {
        var decoration = (TextDecorationCollection)this.SelectedText.GetPropertyValue(Inline.TextDecorationsProperty) == null
          ? TextDecorations.Underline
          : null;
        this.SelectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, decoration);
      }
    }

  }
}
