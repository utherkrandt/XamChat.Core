using System;
using XamChat.Data;
using Foundation;
using UIKit;

namespace XamChat.IOS
{
	public partial class TheirsViewCell : BaseMessageCell
	{

		public TheirsViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void Update(Message message)
		{
			this.message.Text = message.Text;
		}
	}
}
