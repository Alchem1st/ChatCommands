using System;
using UnityEngine;

namespace ChatCommands
{
	protected class Quit : ChatComm
	{
		public override bool hooksSend(RoomChatMessageMessage rcmm) {
			if (rcmm.text.StartsWith ("/quit") || rcmm.text.StartsWith ("/exit")) {
				Application.Quit ();
				return true;
			}
		}
	}
}

