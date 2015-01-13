using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfIntegerTextBox.IntegerTextBox
{
    internal class InternalIntegerTextBox : TextBox
    {
        public InternalIntegerTextBox()
        {
            AddHandler(PreviewMouseLeftButtonDownEvent,
              new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
            AddHandler(GotKeyboardFocusEvent,
              new RoutedEventHandler(SelectAllText), true);
            AddHandler(MouseDoubleClickEvent,
              new RoutedEventHandler(SelectAllText), true);
            AddHandler(LostFocusEvent, new RoutedEventHandler(HandleEmptyString));
            
            TextChanged += UpdateLastGood;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            e.Handled = IsValidNumber(e.Text) == false;
        }

        private static readonly Regex OnlyNumbersRegext = new Regex(@"\d+");

        protected static bool IsValidNumber(string text)
        {
            var isValid = OnlyNumbersRegext.IsMatch(text);
            return isValid;
        }

        private static void SelectivelyIgnoreMouseButton(object sender,
                                                         MouseButtonEventArgs e)
        {
            Debug.WriteLine("PreviewMouseLeftButtonDown");
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent == null) return;
            var textBox = (TextBox)parent;
            if (textBox.IsKeyboardFocusWithin) return;
            // If the text box is not yet focussed, give it the focus and
            // stop further processing of this click event.
            textBox.Focus();
            e.Handled = true;
        }

        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("In SelectAllText");
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }

        public static readonly DependencyProperty EmptyStringBehaviorProperty = DependencyProperty.Register(
            "EmptyStringBehavior", typeof (EmptyStringBehavior), typeof (InternalIntegerTextBox), new PropertyMetadata(default(EmptyStringBehavior)));

        public EmptyStringBehavior EmptyStringBehavior
        {
            get { return (EmptyStringBehavior) GetValue(EmptyStringBehaviorProperty); }
            set { SetValue(EmptyStringBehaviorProperty, value); }
        }

        private void HandleEmptyString(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("LostFocus");
            if (!string.IsNullOrEmpty(Text)) return;
            switch (EmptyStringBehavior)
            {
                case EmptyStringBehavior.UseZero:
                    Text = "0";
                    break;
                case EmptyStringBehavior.LastKnownGood:
                    Text = LastGood;
                    break;
            }
        }

        private string LastGood { get; set; }

        private void UpdateLastGood(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine("TextChanged");
            if (!Validation.GetHasError(this) &&
                !string.IsNullOrEmpty(Text))
                LastGood = Text;
        }
    }
}
