using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace ShortCutDemo
{
  /// <summary>
  /// Use AutomationProperties.Name on the element which tooltip should display the shortcut
  /// And then assign this same name to this attached property "AutomationUIName" on the KeyBinding
  /// The element's tooltip will be then updated with the shortcut
  /// </summary>
  public class ToolTipShortcutBehavior
  {
    public static readonly DependencyProperty AutomationUINameProperty = DependencyProperty.RegisterAttached(
      "AutomationUIName",
      typeof(string),
      typeof(ToolTipShortcutBehavior),
      new FrameworkPropertyMetadata(default(string), OnAutomationUINameChanged));

    private static void OnAutomationUINameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      UpdateSource(d as FrameworkElement, e.NewValue as string);
    }

    public static void SetAutomationUIName(DependencyObject element, string name)
    {
      element.SetValue(AutomationUINameProperty, name);
    }

    public static string GetAutomationUIName(DependencyObject element)
    {
      return (string)element.GetValue(AutomationUINameProperty);
    }

    private static void UpdateSource(FrameworkElement element, string automationUIName)
    {
      var inputBinding = Application.Current.MainWindow.InputBindings
        .OfType<InputBinding>()
        .SingleOrDefault(x => AutomationProperties.GetName(x) == automationUIName);

      string shortcut = string.Empty;
      if (inputBinding?.Gesture is KeyGesture keyGesture)
      {
        shortcut = keyGesture.DisplayString;
        if (string.IsNullOrEmpty(shortcut))
        {
          shortcut = keyGesture.Modifiers == ModifierKeys.None
            ? keyGesture.Key.ToString()
            : $"{keyGesture.Modifiers.ToString()}+{keyGesture.Key}";
        }
      }

      if (!string.IsNullOrEmpty(shortcut))
      {
        string tooltip = (element.ToolTip as string ?? string.Empty) + $" ({shortcut})";
        element.ToolTip = tooltip;
      }
    }
  }
}
