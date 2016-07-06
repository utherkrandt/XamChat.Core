using System;
using XamChat.ViewModels;
using Infrastructure.ServiceLocator;
using UIKit;

namespace XamChat.IOS
{
	public partial class FriendsController : UITableViewController
	{
		readonly FriendsViewModel friendVM = ServiceContainer.Instance.Resolve<FriendsViewModel>();

		public FriendsController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			TableView.Source = new FriendsTableSource();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			try
			{
				await friendVM.GetFriends();
				TableView.ReloadData();
			}
			catch (Exception ex)
			{
				new UIAlertView("Oups", ex.Message, null, "Ok").Show();
			}
		}

		class FriendsTableSource : UITableViewSource
		{
			readonly FriendsViewModel friendVM = ServiceContainer.Instance.Resolve<FriendsViewModel>();
			const string FriendCell = "FriendCell";

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return friendVM.Friends == null ? 0 : friendVM.Friends.Length;
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var friend = friendVM.Friends[indexPath.Row];
				var cell = tableView.DequeueReusableCell(FriendCell);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, FriendCell);
					cell.AccessoryView = UIButton.FromType(UIButtonType.ContactAdd);
					cell.AccessoryView.UserInteractionEnabled = false;
				}
				cell.TextLabel.Text = friend.Username;

				return cell;
			}
		}
	}
}


