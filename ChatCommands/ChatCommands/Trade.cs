using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
    class Trade : ChatComm
    {
        public const string format = "/trade [tradePartner] OR /t [tradePartner]";

        public override bool hooksSend(RoomChatMessageMessage rcmm)
        {            
			if (rcmm.text.ToLower().StartsWith("/trade") || rcmm.text.ToLower().StartsWith("/t"))
			{
                String[] splitted = rcmm.text.Split(' ');
                // Second clause below handles use of Tab on partial names to auto-complete (leaves a trailing space)
                if (splitted.Length == 2 || (splitted.Length == 3 && splitted[2] == ""))
                {
                    String tradePartner = splitted[1];
                    List<ChatRooms.ChatUser> cUsers = App.ArenaChat.ChatRooms.GetCurrentRoomUsers();
                    string id = "";
                    //This loop searches for a user with whatever name was in the command so it can get their userid
                    for (int i = 0; i < cUsers.Count; i++)
                    {
                        if (cUsers[i].name == tradePartner)
                        {
                            id = cUsers[i].id;
                            break;
                        }
                    }
                    if (!(id == ""))
                    {
                        //Sends a trade request to the user specified
                        App.Communicator.sendRequest((Message)new TradeInviteMessage(id));
                        msg("Success: partner = " + tradePartner);
                    }
                    else
                    {
                        msg("Could not find trade partner in this room.");
                    }
                }
                else
                {
                    msg("Unexpected arguments:  Expected:" + format);
                }

				return true;
			}
            return false;
        }
    }
}
