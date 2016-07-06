using System;

namespace XamChat.Data
{
	public class Message
	{

		public long Id {
			get;
			set;
		}
		public long ConversationId {
			get;
			set;
		}
		public long UserId {
			get;
			set;
		}
		public string Username {
			get;
			set;
		}
		public string Text {
			get;
			set;
		}

		public Message ()
		{
		}
	}
}

