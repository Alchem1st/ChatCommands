using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChatCommands
{
	class RoomComm : ChatComm
	{
        public const string joinformat = "/join [room] OR /j [room]";
        public const string partformat = "/part (optional:[room])";
		public override bool hooksSend(RoomChatMessageMessage rcmm)
		{
            try
            {
                String[] splitted = rcmm.text.Split(' ');

                if (splitted[0].Equals("/join") || splitted[0].Equals("/j"))
                {
                    if (splitted.Length == 2)
                    {
                        String roomToJoin = splitted[1];
                        App.ArenaChat.RoomEnter(roomToJoin);
                    }

                    return true;
                }
                else if (splitted[0].Equals("/part"))
                {

                    if (splitted.Length == 1) // leave current room
                    {
                        string croom = App.ArenaChat.ChatRooms.GetCurrentRoom();
                        App.ArenaChat.ChatRooms.LeaveRoom(croom);
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
            catch
            {
                //I had some debugging stuff here earlier, but it never reached this, so it never got an error apparently.
                return false;
            }
		}
	}
}
