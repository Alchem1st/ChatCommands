using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
	interface ChatComm
	{
		bool hooksSend(RoomChatMessageMessage rcmm);

		bool hooksReceive(RoomChatMessageMessage rcmm);
	}
}
