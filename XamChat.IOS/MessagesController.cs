using System;
using XamChat.ViewModels;
using XamChat.Data;
using XamChat.Service;
using Infrastructure.ServiceLocator;

using UIKit;

namespace XamChat.IOS
{
	public partial class MessagesController : UITableViewController
	{
		private readonly MessageViewModel messageVm = ServiceContainer.Instance.Resolve<MessageViewModel>();

		public MessagesController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			this.TableView.Source = new MessageTableSource();
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			this.Title = messageVm.Conversation.Username;
			try
			{
				await messageVm.GetMessages();
				TableView.ReloadData();
			}
			catch (Exception ex)
			{
				new UIAlertView("Oups", ex.Message, null, "Ok").Show();
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
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


