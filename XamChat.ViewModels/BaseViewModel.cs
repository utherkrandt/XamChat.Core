using System;
using Infrastructure.ServiceLocator;
using XamChat.Service;

namespace XamChat.ViewModels
{
	public class BaseViewModel
	{
		protected readonly ISetting settings = ServiceContainer.Instance.Resolve<ISetting>();
		protected readonly IWebService webService = ServiceContainer.Instance.Resolve<IWebService>();

		public event EventHandler OnBusyChanged = delegate{};

		private bool _isBusy = false;
		public bool IsBusy {
			get{ return _isBusy;}
			set{ 
				_isBusy = value;
				OnBusyChanged (this, EventArgs.Empty);
			}
		}


	}
}

