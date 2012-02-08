using System;
using Eto.Forms;
using Eto.Drawing;
using System.Text;

namespace Eto.Test
{
	public class MainForm : Form
	{
		TextArea eventLog;
		Panel contentContainer;

		public TextArea EventLog
		{
			get
			{
				if (eventLog == null) {
					eventLog = new TextArea {
						Size = new Size (100, 100),
						ReadOnly = true,
						Wrap = false
					};
				}
				return eventLog;
			}
		}

		Panel ContentContainer
		{
			get
			{
				if (contentContainer == null) {
					contentContainer = new Panel ();
				}
				return contentContainer;
			}
		}

		public MainForm ()
		{
			this.Text = "Test Application";
			this.Style = "main";
			this.Icon = new Icon (null, "Eto.Test.TestIcon.ico");
			this.Size = new Size (800, 600);
			//this.Opacity = 0.5;

			HandleEvent (MainForm.MaximizedEvent, MainForm.MinimizedEvent, MainForm.ClosedEvent, MainForm.ClosingEvent);

			/* Option 1: use actions to generate menu and toolbar (recommended)
			 */
			GenerateMenuToolBarActions ();


			/* Option 2: generate menu and toolbar directly
			 *
			GenerateMenu();
			
			GenerateToolBar();
			/*
			 */

			this.AddDockedControl (MainContent ());
		}

		Control MainContent ()
		{
			var splitter = new Splitter {
				Position = 200,
				FixedPanel = SplitterFixedPanel.Panel1
			};

			var sectionList = new SectionList (this.ContentContainer);
			// set focus when the form is shown
			this.Shown += delegate {
				sectionList.Focus ();
			};

			splitter.Panel1 = sectionList;
			splitter.Panel2 = RightPane ();

			return splitter;
		}

		Control RightPane ()
		{
			var splitter = new Splitter {
				Orientation = SplitterOrientation.Vertical,
				Position = 400,
				FixedPanel = SplitterFixedPanel.Panel2
			};

			splitter.Panel1 = this.ContentContainer;
			splitter.Panel2 = DockLayout.CreatePanel (this.EventLog, new Padding (5));

			return splitter;
		}

		void GenerateMenuToolBarActions ()
		{
			// use actions to generate menu & toolbar to share logic
			var args = new GenerateActionArgs ();

			// generate actions to use in menus and toolbars
			Application.Instance.GetSystemActions (args);

			args.Actions.Add (new Actions.About ());
			args.Actions.Add (new Actions.Quit ());
			args.Actions.Add (new Actions.Close ());


			// generate and set the menu
			GenerateMenu (args);

			// generate and set the toolbar
			GenerateToolBar (args);
		}

		void GenerateMenu (GenerateActionArgs args)
		{
			var file = args.Menu.FindAddSubMenu ("&File", 100);
			var edit = args.Menu.FindAddSubMenu ("&Edit", 200);
			var window = args.Menu.FindAddSubMenu ("&Window", 900);
			var help = args.Menu.FindAddSubMenu ("Help", 1000);

			if (Generator.ID == "mac") {
				// have a nice OS X style menu

				var main = args.Menu.FindAddSubMenu ("Test Application", 0);
				main.Actions.Add (Actions.About.ActionID, 0);
				main.Actions.AddSeparator ();
				main.Actions.Add ("mac_hide", 700);
				main.Actions.Add ("mac_hideothers", 700);
				main.Actions.Add ("mac_showall", 700);
				main.Actions.AddSeparator (900);
				main.Actions.Add (Actions.Quit.ActionID, 1000);

				file.Actions.Add ("mac_performClose", 900);

				edit.Actions.Add ("mac_undo", 100);
				edit.Actions.Add ("mac_redo", 100);
				edit.Actions.AddSeparator (200);
				edit.Actions.Add ("mac_cut", 200);
				edit.Actions.Add ("mac_copy", 200);
				edit.Actions.Add ("mac_paste", 200);
				edit.Actions.Add ("mac_delete", 200);
				edit.Actions.Add ("mac_selectAll", 200);

				window.Actions.Add ("mac_performMiniaturize");
				window.Actions.Add ("mac_performZoom");
			}
			else {
				// windows/gtk style window
				file.Actions.Add (Actions.Quit.ActionID);

				help.Actions.Add (Actions.About.ActionID);
			}

			this.Menu = args.Menu.GenerateMenuBar ();
		}

		void GenerateToolBar (GenerateActionArgs args)
		{
			args.ToolBar.Add (Actions.Quit.ActionID);
			args.ToolBar.Add (Actions.About.ActionID);
			this.ToolBar = args.ToolBar.GenerateToolBar ();
		}

		#region Generate Menu & Toolbar Manually

		/*
		void GenerateMenu ()
		{
			var menuBar = new MenuBar ();
			
			var file = new ImageMenuItem{ Text = "&File" };
			menuBar.MenuItems.Add (file);
			
			// close
			var close = new ImageMenuItem { Text = "&Close" };
			close.Click += delegate {
				this.Close ();
			};
			file.MenuItems.Add (close);
			
			
			this.Menu = menuBar;
		}
		
		void GenerateToolBar ()
		{
			var toolBar = new ToolBar ();
			
			// close
			var close = new ToolBarButton{ Text = "Close" };
			close.Click += delegate {
				this.Close ();
			};
			toolBar.Items.Add (close);
		}
		*/
		#endregion

		public override void OnMaximized (EventArgs e)
		{
			base.OnMaximized (e);
			Log.Write (this, "Maximized");
		}

		public override void OnMinimized (EventArgs e)
		{
			base.OnMinimized (e);
			Log.Write (this, "Minimized");
		}

		public override void OnClosed (EventArgs e)
		{
			base.OnClosed (e);
			Log.Write (this, "Closed");
		}

		public override void OnClosing (System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing (e);
			Log.Write (this, "Closing");
			/*
			 * Note that on OS X, windows usually close, but the application will keep running.  It is
			 * usually better to handle the Application.OnTerminating event instead.
			 * 
			var result = MessageBox.Show (this, "Are you sure you want to close?", MessageBoxButtons.YesNo);
			if (result == DialogResult.No) e.Cancel = true;
			*/
		}
	}
}

