using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFriend.Objects
{
    class Levels
    {
        public static int GetLevelFromXp(int xp)
        {
            if (xp == 0)
            {
                return 0;
            }
            else if (xp < 2000)
            {
                return 1;
            }
            else if (xp >= 2000 && xp < 4500)
            {
                return 2;
            }
            else if (xp >= 4500)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
}
