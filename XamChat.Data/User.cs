using System;

namespace XamChat.Data
{
	public class User
	{
		public User ()
		{
			Id = 0;
			Username = Password = string.Empty;


		}
		public string Username {
			get;
			set;
		}

		public string Password {
			get;
			set;
		}
		public long Id {
			get;
			set;
		}
	}
}

