using System.Collections.Generic;
using System;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public Item item;
    public bool itemSelected = false;
    public int slot = -1;
    public int UseItem()
    {
        if (itemSelected)
        {
            var itemActions = new Dictionary<Pickup, Action>
            {
                { Pickup.QuartzFlask, () => { GetComponent<QuartzFlask>().Action(); }},
            };

            if (itemActions.ContainsKey(item.pickup))
            {
                try
                {
                    itemActions[item.pickup]();
                }
                catch (Exception MissingComponent)
                {
                    Debug.Log(MissingComponent);
                }
            }
        }

        return slot;
    }
}
