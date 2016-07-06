
using System;
using UIKit;
using XamChat.ViewModels;
using Infrastructure.ServiceLocator;

namespace XamChat.IOS
{
    public partial class LoginController : UIViewController
    {
		private readonly LoginViewModel loginViewModel = ServiceContainer.Instance.Resolve<LoginViewModel>();

        public LoginController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			indicator.Hidden = true;
			loginButton.TouchUpInside += async (sender, e) =>
			 {
				loginViewModel.Username = username.Text;
				loginViewModel.Password = password.Text;
				try
				 {
					 await loginViewModel.Login();
					 PerformSegue("OnLogin", this);
				 }
				catch(Exception ex)
				 {
					 new UIAlertView("Oups", ex.Message, null, "Ok").Show();
				 }
			 };

		}

		private void loginViewModel_OnBusyChanged(object sender, EventArgs e)
		{
			username.Enabled = password.Enabled = loginButton.Enabled = indicator.Hidden = !loginViewModel.IsBusy;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			loginViewModel.OnBusyChanged += loginViewModel_OnBusyChanged;
		}
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			loginViewModel.OnBusyChanged -= loginViewModel_OnBusyChanged;
		}
    }
}