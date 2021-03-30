using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFriend.Objects
{
    public class VirtualFriend
    {
        const string friendStateKey = "friendState";
        const string friendXpKey = "friendXp";
        const string friendNameKey = "friendName";

        public FriendState CurrentFriendState
        {
            get
            {
                if (App.Current.Properties.ContainsKey(friendStateKey))
                {
                    return FriendStates.GetFriendState((string)App.Current.Properties[friendStateKey]);
                }
                else 
                {
                    return FriendState.happy;
                }
            }

            set 
            {
                App.Current.Properties[friendStateKey] = FriendStates.GetFriendString(value);
            }
        }

        public int Xp
        {
            get
            {
                if (App.Current.Properties.ContainsKey(friendXpKey))
                {
                    return (int)App.Current.Properties[friendXpKey];
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                App.Current.Properties[friendXpKey] = value;
            }
        }
      

       /* public Friend()
        { 

        } */

        public void giveFood()
        {
            Xp += 500;
        }

        public void giveSleep() 
        {
            Xp += 750;
        }

        public void giveWork()
        {
            Xp += 175;
        }

        public void giveParty()
        {
            Xp += 250;
        }



    }
}
