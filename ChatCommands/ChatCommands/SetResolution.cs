using System;
using UnityEngine;

namespace ChatCommands
{
	public class SetResolution : ChatComm
	{
		private const string format = "Expected: /setRes [width] [height] (optional:[fullscreen] as ('True'/'False'))";

		public override bool hooksSend(RoomChatMessageMessage rcmm)
		{
			if (rcmm.text.StartsWith ("/setRes"))
			{
				string[] arguments = rcmm.text.Split (' ');

				if (arguments.Length < 3) {
					msg ("To few arguments. "+format);
					return true;
				}

				int width, height;
				bool fullscreen = Screen.fullScreen;
				try {
					width = Convert.ToInt32(arguments[1]);
					height = Convert.ToInt32(arguments[2]);
				} catch {
					msg ("Malformed arguments. "+format);
					return true;
				}

				if (arguments.Length == 4) {
					try {
						fullscreen = Convert.ToBoolean(arguments[3]);
					} catch {
						msg ("Malformed arguments. "+format);
						return true;
					}
				}

				App.Config.SetResolution (width, height, fullscreen);
			}
		}
	}
}

