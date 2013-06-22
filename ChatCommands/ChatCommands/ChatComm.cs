using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
	class ChatComm
	{

		public virtual bool hooksSend(RoomChatMessageMessage rcmm)
		{
			return false;
		}

		public virtual bool hooksReceive(RoomChatMessageMessage rcmm)
		{
			return false;
		}

		public virtual string help()
		{
			return "";
		}

		protected void msg(String txt)
		{
			RoomChatMessageMessage rcmm = new RoomChatMessageMessage();
			rcmm.from = "ChatCommands";
			rcmm.text = "<color=#aa803f>" + txt + "</color>";
			rcmm.roomName = App.ArenaChat.ChatRooms.GetCurrentRoom();

			App.ChatUI.handleMessage(rcmm);
			App.ArenaChat.ChatRooms.ChatMessage(rcmm);
		}
	}
}
