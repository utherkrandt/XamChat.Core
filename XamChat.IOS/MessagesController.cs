using System;
using XamChat.ViewModels;
using XamChat.Data;
using XamChat.Service;
using Infrastructure.ServiceLocator;
using Foundation;
using UIKit;
using System.Drawing;

namespace XamChat.IOS
{
	public partial class MessagesController : UITableViewController
	{
		private readonly MessageViewModel messageVm = ServiceContainer.Instance.Resolve<MessageViewModel>();

		private UIToolbar toolBar;
		private UITextField textField;
		private UIBarButtonItem btnSend;
		private NSObject willShowObserver;
		private NSObject willHideObserver;

		public MessagesController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			GenerateUI();
			this.TableView.Source = new MessageTableSource();
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			this.Title = messageVm.Conversation.Username;

			willShowObserver = UIKeyboard.Notifications.ObserveWillShow((sender, e) => OnKeyboardNotif(e));
			willHideObserver = UIKeyboard.Notifications.ObserveWillHide((sender, e) => OnKeyboardNotif(e));

			messageVm.OnBusyChanged += OnBusyChanged;
			try
			{
				await messageVm.GetMessages();
				TableView.ReloadData();
				textField.BecomeFirstResponder();
			}
			catch (Exception ex)
			{
				new UIAlertView("Oups", ex.Message, null, "Ok").Show();
			}
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			if (willShowObserver != null)
			{
				willShowObserver.Dispose();
				willShowObserver = null;
			}

			if (willHideObserver != null)
			{
				willHideObserver.Dispose();
				willHideObserver = null;
			}

			messageVm.OnBusyChanged -= OnBusyChanged;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void OnBusyChanged(object sender, EventArgs e)
		{
			textField.Enabled = btnSend.Enabled = !messageVm.IsBusy;
		}

		private void OnKeyboardNotif(UIKeyboardEventArgs e)
		{
			bool isShow = e.Notification.Name.Equals(UIKeyboard.WillShowNotification);

			UIView.BeginAnimations("AnimateForKeyboard");
			UIView.SetAnimationDuration(e.AnimationDuration);
			UIView.SetAnimationCurve(e.AnimationCurve);

			if (isShow)
			{
				var keyboardFrame = e.FrameEnd;
				var frame = TableView.Frame;
				frame.Height -= keyboardFrame.Height;
				TableView.Frame = frame;
				frame = toolBar.Frame;
				frame.Y -= keyboardFrame.Height;
				toolBar.Frame = frame;
			}
			else
			{
				var keyboardFrame = e.FrameBegin;
				var frame = TableView.Frame;
				frame.Height += keyboardFrame.Height;
				TableView.Frame = frame;
				frame = toolBar.Frame;
				frame.Y += keyboardFrame.Height;
				toolBar.Frame = frame;
			}
			UIView.CommitAnimations();
			ScroolToEnd();
		}

		private void ScroolToEnd()
		{
			TableView.ContentOffset = new PointF(0, (float)(TableView.ContentSize.Height - TableView.Frame.Height));
		}

		private void GenerateUI()
		{
			textField = new UITextField(new RectangleF(0, 0, 144, 32));
			textField.BorderStyle = UITextBorderStyle.RoundedRect;
			textField.ReturnKeyType = UIReturnKeyType.Send;
			textField.ShouldReturn = _ =>
			{
				Send();
				return false;
			};

			btnSend = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, (sender, ev) => Send());
			toolBar = new UIToolbar(new RectangleF(0, (float)TableView.Frame.Height - 44, (float)TableView.Frame.Width, 44));
			toolBar.Items = new UIBarButtonItem[] 
			{ 
				new UIBarButtonItem(textField), 
				btnSend 
			};

			NavigationController.View.AddSubview(toolBar);
			TableView.TableFooterView = new UIView(new RectangleF(0, 0,(float) TableView.Frame.Width, 44) )
			{ BackgroundColor = UIColor.Clear};
		}

		private async void Send()
		{
			if (string.IsNullOrEmpty(textField.Text))
			{
				textField.BecomeFirstResponder();
				return;
			}

			messageVm.Text = textField.Text;
			await messageVm.SendMessage();

			messageVm.Text = textField.Text= string.Empty;

			TableView.ReloadData();
			textField.ResignFirstResponder();
			ScroolToEnd();

		}

		class MessageTableSource : UITableViewSource
		{
			private readonly MessageViewModel messageVm = ServiceContainer.Instance.Resolve<MessageViewModel>();
			private readonly ISetting setting = ServiceContainer.Instance.Resolve<ISetting>();

			const string MyCell = "MyViewCell";
			const string TheirCell = "TheirsViewCell";
			
			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return messageVm.Messages != null ? messageVm.Messages.Length:0;
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var message = messageVm.Messages[indexPath.Row];
				bool isMine = message.UserId.Equals(setting.User.Id);
				var cell = tableView.DequeueReusableCell( isMine? MyCell : TheirCell) as BaseMessageCell;
				Console.WriteLine(cell.ReuseIdentifier.ToString());
				cell.Update(message);
				return cell;

			}
		}
	}
}


