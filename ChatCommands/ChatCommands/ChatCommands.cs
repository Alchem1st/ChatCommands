﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommands
{
    public class ChatCommands : BaseMod
    {
		private List<ChatComm> commands = new List<ChatComm>();

		public ChatCommands()
		{
			commands.Add(new Ignore());
			commands.Add(new RoomComm());
		}

		public static String GetName()
		{
			return "ChatCommands";
		}

		public static int GetVersion()
		{
			return 1;
		}

		public static MethodDefinition[] GetHooks(TypeDefinitionCollection scrollsTypes, int version)
		{
			return new MethodDefinition[] {
					scrollsTypes["ChatRooms"].Methods.GetMethod("ChatMessage", new Type[]{typeof(RoomChatMessageMessage)}),
					scrollsTypes["Communicator"].Methods.GetMethod("sendRequest", new Type[]{typeof(Message)})};
		}

		public override bool BeforeInvoke(InvocationInfo info, out object returnValue)
		{
			returnValue = null;

			if (info.targetMethod.Equals("ChatMessage")) // ChatMessage (received) in ChatRooms
			{
				RoomChatMessageMessage rcmm = (RoomChatMessageMessage)info.arguments[0];

				return hooks(false, rcmm);
			} 
			else if (info.targetMethod.Equals("sendRequest"))
			{
				if (info.arguments[0] is RoomChatMessageMessage)
				{
					RoomChatMessageMessage rcmm = (RoomChatMessageMessage)info.arguments[0];

					return hooks(true, rcmm);
				}
			}
			return false;
		}

		/**
		 * returns true when the text message is a command for one of the functions
		 */
		private bool hooks(bool sending, RoomChatMessageMessage rcmm)
		{
			bool h = false;
			foreach (ChatComm cc in commands)
			{
				bool hooksSingle = sending ? cc.hooksSend(rcmm) : cc.hooksReceive(rcmm);

				h |= hooksSingle;
			}
			return h;
		}
    }
}