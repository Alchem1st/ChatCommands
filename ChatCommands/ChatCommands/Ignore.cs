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
			String[] splitted = rcmm.text.Split(' ');
			// heh heh, allow mods to still use /mute
			if (splitted[0].Equals("/ignore") || (splitted[0].Equals("/mute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
				if (splitted.Length == 1) // just "/ignore", show ignore list
				{
					printIgnoreList();
				}
				else
				{
					// get username from text

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
				}
				return true;
			}
			else if (splitted[0].Equals("/unignore") || (splitted[0].Equals("/unmute") && App.MyProfile.ProfileInfo.adminRole == AdminRole.None))
			{
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
			else if (splitted[0].Equals("/ignorelist") || splitted[0].Equals("/ignoring"))
			{
				printIgnoreList();
				return true;
			}

			return false;
		}

		private void printIgnoreList()
		{
			if (ignoring.Count == 0)
			{
				msg("You're not ignoring anyone.");
			}
			else
			{
				msg("Players currently ignored: " + String.Join(", ", ignoring.ToArray()));
			}
		}

		public override bool hooksReceive(RoomChatMessageMessage rcmm)
		{
			// return true if sender is ignored :)
			return ignoring.Contains(rcmm.from.ToLower());
		}
	}
}
