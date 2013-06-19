using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommands
{
	interface ChatComm
	{
		public bool hooksSend(RoomChatMessageMessage rcmm);

		public bool hooksReceive(RoomChatMessageMessage rcmm);
	}
}
