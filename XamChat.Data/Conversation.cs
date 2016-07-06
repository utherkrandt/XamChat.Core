using System;

namespace XamChat.Data
{
	public class Conversation
	{
		public long Id {
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

		public Conversation ()
		{
		}
	}
}

