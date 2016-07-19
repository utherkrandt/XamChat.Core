using System;
using System.Threading.Tasks;
using XamChat.Data;
using System.Collections.Generic;
using System.Linq;

namespace XamChat.ViewModels
{
	public class FriendsViewModel:BaseViewModel
	{
		public User[] Friends {
			get;
			set;
		}
		public string Username {
			get;
			set;
		}

		private void ValidateUserInput()
		{
			if (string.IsNullOrEmpty (Username))
				throw new ArgumentNullException ("Username cannot be null");	
		}

		private void CheckUserLogged()
		{
			if (settings.User == null)
				throw new InvalidOperationException ("You must be logged to do it");
		}


		public async Task AddFriend()
		{
			CheckUserLogged ();
			ValidateUserInput ();
			IsBusy = true;
			try {
				var friend = await webService.AddFriend(
					new User
					{ 
						Username = this.Username,
					});
				var friends = new List<User>();

				if(Friends!=null)
					friends.AddRange(Friends);
				friends.Add(friend);
				Friends = friends.OrderBy(x=> x.Username).ToArray();					
			} finally {
				IsBusy = false;
			}
		}

		public async Task GetFriends()
		{
			CheckUserLogged ();
			IsBusy = true;
			try {
				Friends = await webService.GetFriendsNotInConversation(settings.User.Id);
			} finally {
				IsBusy = false;
			}
		}
	
	}
}

