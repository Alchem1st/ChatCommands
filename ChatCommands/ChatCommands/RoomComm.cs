using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
	class RoomComm : ChatComm
	{
		public override bool hooksSend(RoomChatMessageMessage rcmm)
		{
			if (rcmm.text.StartsWith("/join"))
			{
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length == 2)
				{
					String roomToJoin = splitted[1];
					App.ArenaChat.RoomEnter(roomToJoin);
				}

				return true;
			}
			else if (rcmm.text.StartsWith("/part"))
			{
				String[] splitted = rcmm.text.Split(' ');

				if (splitted.Length == 1) // leave current room
				{
					App.ArenaChat.ChatRooms.LeaveRoom(App.ArenaChat.ChatRooms.GetCurrentRoom());
				}
				else if (splitted.Length == 2)
				{
					String roomToPart = splitted[1];
					App.ArenaChat.ChatRooms.LeaveRoom(roomToPart);
				}

				return true;
			}
			return false;
		}
	}
}
