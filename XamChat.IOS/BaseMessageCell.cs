using System;

using Foundation;
using UIKit;
using XamChat.Data;

namespace XamChat.IOS
{
	public partial class BaseMessageCell : UITableViewCell
	{
		public BaseMessageCell(IntPtr handle) : base(handle)
		{

		}

		public virtual void Update(Message message)
		{

		}
	}
}
