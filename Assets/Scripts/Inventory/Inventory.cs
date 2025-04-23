using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();
    public int capacity = 20;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        instance = this;
    }

    public bool Add(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory full");
            return false;
        }

        items.Add(item);
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}