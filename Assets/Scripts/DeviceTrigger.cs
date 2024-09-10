using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    private const string ItemName = "Key";

    [SerializeField]
    private GameObject[] _targets;

    [SerializeField]
    private string _activateMethod = "Activate";

    [SerializeField]
    private string _deactivateMethod = "Deactivate";

    [SerializeField]
    private bool _requireKey; 

    private void OnTriggerEnter(Collider other)
    {
        if (_requireKey && Managers.InventoryManager.EquippedItem != ItemName)
        {
            return;
        }

        foreach (GameObject target in _targets)
        {
            target.SendMessage(_activateMethod);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in _targets)
        {
            target.SendMessage(_deactivateMethod);
        }
    }
}
