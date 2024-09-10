using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    [SerializeField]
    private int _positionX = 10;

    [SerializeField]
    private int _positionY = 10;

    [SerializeField]
    private int _width = 100;

    [SerializeField]
    private int _height = 30;

    [SerializeField]
    private int _buffer = 10;

    private void OnGUI()
    {
        int positionX = _positionX;
        int positionY = _positionY;

        List<string> items = Managers.InventoryManager.GetItems();

        if (items.Count == 0)
        {
            GUI.Box(new Rect(_positionX, _positionY, _width, _height), "No Items");
        }

        foreach (string item in items) 
        { 
            int itemCount = Managers.InventoryManager.GetItemCount(item);

            Texture2D iconImage = Resources.Load<Texture2D>($"Icons/{item}");

            GUI.Box(new Rect(positionX, positionY, _width, _height), new GUIContent($"({itemCount})", iconImage));

            positionX += _width + _buffer;
        }

        string equippedtem = Managers.InventoryManager.EquippedItem;

        if (equippedtem != null)
        {
            positionX = Screen.width - (_width + _buffer);

            Texture2D iconImage = Resources.Load<Texture2D>($"Icons/{equippedtem}");

            GUI.Box(new Rect(positionX, _positionY, _width, _height), new GUIContent("Equipped", iconImage));
        }

        positionX = _positionX;
        positionY += _height + _buffer;

        foreach (string item in items) 
        {
            if (GUI.Button(new Rect(positionX, positionY, _width, _height), $"Equip {item}"))
            {
                Managers.InventoryManager.EquipItem(item);
            }

            if (item == "Health")
            {
                if (GUI.Button(new Rect(positionX, positionY + _height + _buffer, _width, _height), "Use Health"))
                {
                    Managers.InventoryManager.ConsumeItem(item);
                    Managers.PlayerManager.ChangeHealth(25);
                }
            }

            positionX += _width + _buffer;
        }
    }
}
