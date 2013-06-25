using JsonFx.Json;
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
			String[] splitted = rcmm.text.Split(' ');
			if (splitted[0].Equals("/player"))
			{
				if (splitted.Length >= 2)
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
			WebClientTimeOut wc = new WebClientTimeOut();
			wc.TimeOut = 5000;
			wc.DownloadStringCompleted += (sender, e) =>
				{
					proc(e.Result, playerName);
				};
			wc.DownloadStringAsync(new Uri("http://a.scrollsguide.com/player?fields=rank,rating,name&name=" + playerName));
		}

		private void proc(String result, String playerName)
		{
			try
			{
				APIResult ar = (APIResult)new JsonReader().Read(result, System.Type.GetType("APIResult"));
				if (ar.msg.Equals("success"))
				{
					msg(String.Format("{0} is ranked {1} with a rating of {2}.", ar.data.name, ar.data.rank, ar.data.rating));
				}
				else
				{
					msg(String.Format("Failed to load data for player {0}.", playerName));
				}
			}
			catch
			{
				msg(String.Format("Failed to load data for player {0}.", playerName));
			}
		}
	}
}
