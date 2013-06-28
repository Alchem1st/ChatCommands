using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatCommands
{
    class Help : ChatComm
    {
        public override bool hooksSend(RoomChatMessageMessage rcmm)
        {
            if (App.MyProfile.ProfileInfo.adminRole == AdminRole.None && (rcmm.text.ToLower().Equals("/help") || rcmm.text.ToLower().StartsWith("/help ")))
            {
                string[] splitted = rcmm.text.Split(' ');
                //Used to determine whether the right help message could be shown, if not, shows the default
                bool helped = false;
                //Checks if they typed a command after /help
                if (splitted.Length == 2)
                {
                    switch (splitted[1].ToLower())
                    {
                        case "trade":
                        case "t":
                            msg("The trade command is used to send a trade request to another player in the same room.");
                            msg(string.Concat("Usage: ", Trade.format));
                            helped = true;
                            break;
                        case "ignorelist":
                        case "ignore":
                        case "unignore":
                            msg("The ignore command is used to ignore the messages of another player so you don't see them.");
                            msg("To stop ignoring someone, you can use the unignore command.");
                            msg("To see who you are ignoreing, you can use the ignorelist command.");
                            msg("The list of ignored players is not saved between game sessions.");
                            msg(string.Concat("Usage: ", Ignore.format));
                            helped = true;
                            break;
                        case "quit":
                        case "exit":
                            msg("The quit command closes scrolls.");
                            msg(string.Concat("Usage: ", Quit.format));
                            helped = true;
                            break;
                        case "setres":
                            msg("The setRes command sets the resolution of the game as long as it is a valid resolution.");
                            msg(string.Concat("Usage: ", SetResolution.format));
                            helped = true;
                            break;
                        case "join":
                        case "j":
                            msg("The join command enters the chatroom specified as long as it is not full.");
                            msg(string.Concat("Usage: ", RoomComm.joinformat));
                            helped = true;
                            break;
                        case "part":
                            msg("The part command leaves the chatroom specified, or the current chatroom by default.");
                            msg(string.Concat("Usage: ", RoomComm.partformat));
                            helped = true;
                            break;
                        case "player":
                            msg("The player command retrieves the ranked stats of a player from scrollsguide.com.");
                            msg(string.Concat("Usage: ", Player.format));
                            helped = true;
                            break;
                        case "help":
                            msg("Really? You need help using the help command? :)");
                            helped = true;
                            break;
                        default:
                            msg(string.Format("Command {0} not recognized, showing default help message.",splitted[1].ToLower()));
                            break;
                    }
                }
                if (!(splitted.Length == 2) || helped == false)
                {
                    //Checks if they typed something weird after /help (example: /help /trade randomperson)
                    if (splitted.Length > 2)
                    {
                        msg("Format not recognized, showing default help message.");
                    }
                    msg(string.Concat("ChatCommands Version: ", Convert.ToString(ChatCommands.GetVersion())));
                    msg("For more information on a specific command, type /help [command]");
                    msg("Commands: exit, help, ignore, ignorelist, j, join, part, player, quit, setRes, t, trade, unignore");
                }
                return true;
            }
            return false;
        }
    }
}
