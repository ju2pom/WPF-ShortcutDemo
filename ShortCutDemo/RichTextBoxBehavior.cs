using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace ShortCutDemo
{
  public class RichTextBoxBehavior : Behavior<RichTextBox>
  {
    public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register(
      "SelectedText",
      typeof(TextSelection),
      typeof(RichTextBoxBehavior),
      new PropertyMetadata(default(TextSelection)));

    public TextSelection SelectedText
    {
      get { return (TextSelection)this.GetValue(SelectedTextProperty); }
      set { this.SetValue(SelectedTextProperty, value); }
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      if (this.AssociatedObject != null)
      {
        this.AssociatedObject.Unloaded += this.OnUnLoaded;
        this.AssociatedObject.SelectionChanged += OnSelectionChanged;
      }
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.Unloaded -= this.OnUnLoaded;
      this.AssociatedObject.SelectionChanged -= OnSelectionChanged;
    }

    private void OnUnLoaded(object sender, RoutedEventArgs e)
    {
      this.OnDetaching();
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
      this.SelectedText = this.AssociatedObject.Selection;
    }
  }
}
