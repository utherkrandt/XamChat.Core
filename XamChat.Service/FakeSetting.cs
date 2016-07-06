using System;
using System.Threading;

namespace XamChat.Service
{
	public class FakeSetting:ISetting
	{
		private XamChat.Data.User _user;
		public FakeSetting ()
		{
		}

		#region ISetting implementation

		public void Save ()
		{
			//Nothing to do
		}

		public XamChat.Data.User User {
			get {
				return _user;
			}
			set {
				_user = value;
			}
		}

		#endregion
	}
}

