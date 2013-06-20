using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommands
{
	class RoomComm : ChatComm
	{
		public bool hooksSend(RoomChatMessageMessage rcmm)
		{
			if (rcmm.text.startsWith("/join"))
			{
				String splitted = rcmm.text.Split(" ");

				if (splitted.Length == 2)
				{
					String roomToJoin = splitted[1];
					App.ArenaChat.RoomEnter(roomToJoin);
				}

				return true;
			}
			else if (rcmm.text.startsWith("/part"))
			{
				String splitted = rcmm.text.Split(" ");

				if (splitted.Length == 2)
				{
					String roomToPart = splitted[1];
					App.ArenaChat.chatRooms.LeaveRoom(roomToPart);
				}
			}
			return false;
		}

		// nothing to do here
		public bool hooksReceive(RoomChatMessageMessage rcmm)
		{
			return false;
		}
	}
}
