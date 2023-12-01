using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public static PlayerInventory I {get; private set;}
    
    [Serializable]
    public class Item
    {
        public string m_itemName;
        public int m_itemAmount;
    }

    public Item[] m_Items;

    private void Awake()
    {
#region Singleton
        
        if(I == null)
        {
            I = this;
        }
        else if(I != null && I != this)
        {
            Destroy(this);
        }
        
#endregion
    }

    public void AddOrRemoveItem(string _itemName, int _itemAmount)
    {
        
        Item item = Array.Find(m_Items, item => item.m_itemName == _itemName);

        if(item == null) return;

        item.m_itemAmount += _itemAmount;
        item.m_itemAmount = Mathf.Clamp(item.m_itemAmount, 0, 100000000);
    }
}
