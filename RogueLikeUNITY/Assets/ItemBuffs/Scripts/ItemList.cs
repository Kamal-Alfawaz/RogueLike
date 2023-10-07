using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemList
{
    public Item item;
    public string name;
    public int count;

    public ItemList(Item newItem, string newName, int newCount){
        item = newItem;
        name = newName;
        count = newCount;
    }

}
