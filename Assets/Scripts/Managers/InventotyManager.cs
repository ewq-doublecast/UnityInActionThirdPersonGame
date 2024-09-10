using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventotyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus ManagerStatus { get; private set; }

    public string EquippedItem { get; private set; }

    private Dictionary<string, int> _items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        _items = new Dictionary<string, int>();

        ManagerStatus = ManagerStatus.Started;
    }

    public List<string> GetItems()
    {
        return _items.Keys.ToList();
    }

    public int GetItemCount(string itemName)
    {
        if (_items.ContainsKey(itemName))
        {
            return _items[itemName];
        }

        return 0;
    }

    public bool EquipItem(string itemName)
    {
        if (_items.ContainsKey(itemName) && EquippedItem != itemName)
        {
            EquippedItem = itemName;
            Debug.Log($"Equipped {itemName}");
            return true;
        }

        EquippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItem(string itemName)
    {
        if (_items.ContainsKey(itemName)) 
        { 
            _items[itemName]--;

            if (_items[itemName] == 0) 
            {
                _items.Remove(itemName);
            }
        }
        else
        {
            Debug.Log($"Cannot consume {itemName}");
            return false;
        }

        DisplayItems();
        return true;
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name]++;
        }
        else 
        {
            _items[name] = 1;
        }

        DisplayItems();
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";

        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += $"{item.Key} ({item.Value}) ";
        }

        Debug.Log(itemDisplay);
    }
}
