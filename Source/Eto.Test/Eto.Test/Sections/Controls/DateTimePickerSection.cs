using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.Test.Sections.Controls
{
	public class DateTimePickerSection : Panel
	{
		public DateTimePickerSection ()
		{
			var layout = new DynamicLayout (this);
			
			layout.AddRow (new Label{ Text = "Default" }, Default ());
			
			layout.AddRow (new Label{ Text = "Date" }, Date ());
			layout.AddRow (new Label{ Text = "Time" }, Time ());
			layout.AddRow (new Label{ Text = "Date/Time" }, DateTime ());
			
			// growing space at end is blank!
			layout.Add (null);
		}
		
		Control Default ()
		{
			var control = new DateTimePicker{  };
			LogEvents (control);
			return control;
		}
		
		Control Date ()
		{
			var control = new DateTimePicker{ Mode = DateTimePickerMode.Date };
			LogEvents (control);
			return control;
		}
		
		Control Time ()
		{
			var control = new DateTimePicker{ Mode = DateTimePickerMode.Time };
			LogEvents (control);
			return control;
		}
		
		Control DateTime ()
		{
			var control = new DateTimePicker{ Mode = DateTimePickerMode.DateTime };
			LogEvents (control);
			return control;
		}
		
		void LogEvents (DateTimePicker control)
		{
			control.ValueChanged += delegate {
				Log.Write (control, "ValueChanged, Value: {0}", control.Value);
			};
		}
	}
}

