using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ChatCommands
{
	class APIComm : ChatComm
	{
		public override bool hooksSend(RoomChatMessageMessage rcmm)
		{
			if (rcmm.text.StartsWith("/player"))
			{
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length == 2)
				{
					String playerName = splitted[1];
					loadPlayerInfo(playerName);
				}

				return true;
			}
			return false;
		}

		private void loadPlayerInfo(String playerName)
		{
			new WebClient().DownloadString("http://a.scrollsguide.com/player?name=" + playerName);
		}
	}
}
