using System;
using XamChat.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamChat.ViewModels
{
	public class MessageViewModel : BaseViewModel
	{
		public Conversation[] Conversations
		{
			get;
			private set;
		}	

		public Message[] Messages
		{
			get;
			private set;
		}

		public string Text
		{
			get;
			set;
		}

		public Conversation Conversation
		{
			get;
			set;
		}

		private void CheckUserLogged()
		{
			if (settings.User == null)
				throw new InvalidOperationException("You must be logged to do it");
		}

		private void CheckConversation()
		{
			if (Conversation == null)
				throw new InvalidOperationException("No Conversation");
		}
		private void CheckMessage()
		{
			if (string.IsNullOrEmpty(Text))
				throw new InvalidOperationException("Message blank");
		}


		public async Task GetConversation()
		{
			CheckUserLogged();
			IsBusy = true;
			try
			{
				Conversations = await webService.GetConversation(settings.User.Id);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public async Task GetMessages()
		{
			CheckConversation();
			IsBusy = true;
			try
			{
				Messages = await webService.GetMessages(Conversation.Id);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public async Task SendMessage()
		{
			CheckUserLogged();
			CheckConversation();
			CheckMessage();

			IsBusy = true;
			try
			{
				var message = new Message 
				{
					UserId = settings.User.Id, 
					ConversationId = Conversation.Id, 
					Text = this.Text,
					Username = settings.User.Username
				};

				message = await webService.SendMessage(message);
				var messages = new List<Message>();
				messages.AddRange(Messages);
				if (message != null)
					messages.Add(message);
				Messages = messages.ToArray();
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}

