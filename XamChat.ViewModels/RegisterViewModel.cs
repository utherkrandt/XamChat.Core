using System;
using System.Threading.Tasks;
using XamChat.Data;

namespace XamChat.ViewModels
{
	public class RegisterViewModel:BaseViewModel
	{
		public string Username {
			get;
			set;
		}

		public string Password {
			get;
			set;
		}

		public string ConfirmPassword {
			get;
			set;
		}


		public RegisterViewModel ()
		{
		}

		public async Task Register()
		{
			ValidateUserInput ();
			IsBusy = true;
			try {
				settings.User = await webService.Register(new User
					{ 
						Username = this.Username, 
						Password = this.Password
					});
				settings.Save();
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
			if (!Password.Equals(ConfirmPassword)) {
				throw new ArgumentException("Password and confirmPasswotd are different");
			}
		}
	}
}

