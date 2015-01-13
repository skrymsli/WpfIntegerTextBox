# WpfIntegerTextBox

A text box for WPF that works thusly:

* Only positive integers are allowed.
* Set **MinValue** to restrict the minimum value.
* Set **MaxValue** to restrict the maximum value.
* Set **EmptyStringBehavior** to specify what happens if the user clears out the string, then tabs to another control.  The behavior can be one of the following:
  * **DoNothing**: Don't do anything.
  * **UseMin**: Use the minimum value.
  * **LastKnownGood**: Reset the value to the last valid value.
