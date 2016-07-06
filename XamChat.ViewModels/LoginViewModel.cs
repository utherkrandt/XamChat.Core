using System;
using System.Threading.Tasks;

namespace XamChat.ViewModels
{
	public class LoginViewModel:BaseViewModel
	{
		public string Username {
			get;
			set;
		}

		public string Password {
			get;
			set;
		}


		public LoginViewModel ()
		{
		}


		public async Task Login ()
		{
			ValidateUserInput ();
			IsBusy = true;
			try {

				var user = await webService.Login(Username, Password);
				if (user != null)
				{
					settings.User = user;
					settings.Save();
				}
				else
					throw new Exception("Unknow user. Please register");

			} finally {
				IsBusy = false;
			}
		}


		private void ValidateUserInput()
		{
			if (string.IsNullOrEmpty (Username))
				throw new ArgumentNullException ("Username cannot be null");

			if (string.IsNullOrEmpty (Password))
				throw new ArgumentNullException ("Password cannot be null");
		}

	}
}

