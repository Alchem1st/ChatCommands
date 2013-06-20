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

		public override bool hooksSend(RoomChatMessageMessage rcmm)
		{
			// heh heh, allow mods to still use /mute
			if (rcmm.text.StartsWith("/ignore") || (rcmm.text.StartsWith("/mute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
				// get username from text
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length >= 2)
				{
					String usernameToIgnore = splitted[1].ToLower();
					// let's not ignore ourselves
					if (!ignoring.Contains(usernameToIgnore) && usernameToIgnore != App.MyProfile.ProfileInfo.name.ToLower())
					{
						ignoring.Add(usernameToIgnore);

						// splitted[1] instead of usernameToIgnore because of caps :)
						msg("Added " + splitted[1] + " to the ignore list.");
					}
					else
					{
						msg(splitted[1] + " is already on the ignore list.");
					}
				}
				return true;
			}
			else if (rcmm.text.StartsWith("/unignore") || (rcmm.text.StartsWith("/unmute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
				// get username from text
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length >= 2) // >2, kinda strips any trailing space ;)
				{
					if (ignoring.Contains(splitted[1].ToLower()))
					{
						ignoring.Remove(splitted[1].ToLower());
					}
					msg("Removed " + splitted[1] + " from the ignore list.");
				}
				return true;
			}

			return false;
		}

		public override bool hooksReceive(RoomChatMessageMessage rcmm)
		{
			// return true if sender is ignored :)
			return ignoring.Contains(rcmm.from.ToLower());
		}
	}
}
