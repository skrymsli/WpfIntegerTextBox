using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfIntegerTextBox.IntegerTextBox
{
    /// <summary>
    /// Interaction logic for IntegerTextBox.xaml
    /// </summary>
    public partial class IntegerTextBox 
    {
        public IntegerTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue", typeof (int), typeof (IntegerTextBox), new PropertyMetadata(0));

        public int MinValue
        {
            get { return (int) GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue", typeof (int), typeof (IntegerTextBox), new PropertyMetadata(int.MaxValue));

        public int MaxValue
        {
            get { return (int) GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(int), typeof(IntegerTextBox), new PropertyMetadata(default(int), null, CoerceMinMax));

        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static object CoerceMinMax(DependencyObject d, object value)
        {
            var This = d as IntegerTextBox;
            if (This == null) return value;

            var thisValue = (int) value;
            if (thisValue < This.MinValue)
                return This.MinValue;
            return thisValue > This.MaxValue ? This.MaxValue : value;
        }

        public static readonly DependencyProperty EmptyStringBehaviorProperty = DependencyProperty.Register(
            "EmptyStringBehavior", typeof (EmptyStringBehavior), typeof (IntegerTextBox), new PropertyMetadata(EmptyStringBehavior.DoNothing));

        public EmptyStringBehavior EmptyStringBehavior
        {
            get { return (EmptyStringBehavior) GetValue(EmptyStringBehaviorProperty); }
            set { SetValue(EmptyStringBehaviorProperty, value); }
        }

    }
}
