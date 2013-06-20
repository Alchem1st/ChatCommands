using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
	class Ignore : ChatComm
	{
		// list of usernames that are ignored/muted
		private List<String> ignoring = new List<String>();

		public bool hooksSend(RoomChatMessageMessage rcmm)
		{

			Console.WriteLine("got hooksSend Ignore: " + rcmm.text + "    " + rcmm.getRawText());

			// heh heh, allow mods to still use /mute
			if (rcmm.text.StartsWith("/ignore") || (rcmm.text.StartsWith("/mute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
				// get username from text
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length == 2)
				{
					String usernameToIgnore = splitted[1];
					if (!ignoring.Contains(usernameToIgnore))
					{
						ignoring.Add(usernameToIgnore);
					}
				}
				return true;
			}
			else if (rcmm.text.StartsWith("/unignore") || (rcmm.text.StartsWith("/unmute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
				// get username from text
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length == 2)
				{
					ignoring.Remove(splitted[1]);
				}
				return true;
			}

			return false;
		}

		public bool hooksReceive(RoomChatMessageMessage rcmm)
		{
			// return true if sender is ignored :)
			return ignoring.Contains(rcmm.from);
		}
	}
}
