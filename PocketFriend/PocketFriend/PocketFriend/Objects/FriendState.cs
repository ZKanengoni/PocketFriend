using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFriend.Objects
{
    public enum FriendState
    {
        happy,
        sad,
        depressed
    }

    class FriendStates
    {
        public static string GetFriendString(FriendState friendState) 
        {
            switch (friendState) 
            {
                case FriendState.happy:
                    return "happy";
                case FriendState.sad:
                    return "sad";
                case FriendState.depressed:
                    return "depressed";
                default:
                    return "okay";
            }
        }

        public static FriendState GetFriendState(string friendString)
        {
            switch (friendString)
            {
                case "happy":
                    return FriendState.happy;
                case "sad":
                    return FriendState.sad;
                case "depressed":
                    return FriendState.depressed;
                default:
                    return FriendState.happy;
            }
        }
    }

}
