using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public class InventoryItemUse : Singleton<InventoryItemUse>
    {
        public ItemData Data;
    }

}
