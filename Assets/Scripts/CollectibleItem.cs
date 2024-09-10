using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private string _itemName;

    private void OnTriggerEnter(Collider other)
    {
        Managers.InventoryManager.AddItem(_itemName);
        Destroy(gameObject);
    }
}
