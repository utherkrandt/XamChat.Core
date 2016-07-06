using System;
using UIKit;
using XamChat.ViewModels;
using Infrastructure.ServiceLocator;

namespace XamChat.IOS
{
	public partial class ConversationController : UITableViewController
	{
		private readonly MessageViewModel messageViewModel = ServiceContainer.Instance.Resolve<MessageViewModel>();

		public ConversationController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			TableView.Source = new MessageTableSource(this);
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			try
			{
				await messageViewModel.GetConversation();
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
			private ConversationController controller;
			private readonly MessageViewModel messageVm = ServiceContainer.Instance.Resolve<MessageViewModel>();
			const string CellName = "ConversationCell";


			public MessageTableSource(ConversationController conversationController)
			{
				this.controller = conversationController;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return messageVm.Conversations!=null ? messageVm.Conversations.Length : 0;
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var message = messageVm.Conversations[indexPath.Row];
				var cell = tableView.DequeueReusableCell(CellName);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellName);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				cell.TextLabel.Text = message.Username;
				return cell;
			}

			public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var conversation = messageVm.Conversations[indexPath.Row];
				messageVm.Conversation = conversation;
				controller.PerformSegue("OnConversation",null);
			}
		}
	}
}


