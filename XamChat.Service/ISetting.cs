using System;
using XamChat.Data;

namespace XamChat.Service
{
	public interface ISetting
	{
		User User {get; set; }
		void Save();
	}
}

