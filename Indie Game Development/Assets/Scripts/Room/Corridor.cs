using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obsolete
{
    public class Corridor : Room_Framework
    {
        public void Reset()
        {
            roomSize = new Vector2Int(1, 1);
            buildType = BuildType.Corridor;
            cost = 0;
        }
    }
}
