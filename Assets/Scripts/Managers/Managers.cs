using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventotyManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager PlayerManager;
    public static InventotyManager InventoryManager;

    private List<IGameManager> _managers;

    private void Awake()
    {
        PlayerManager = GetComponent<PlayerManager>();
        InventoryManager = GetComponent<InventotyManager>();

        _managers = new List<IGameManager>();

        _managers.Add(PlayerManager);
        _managers.Add(InventoryManager);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _managers) 
        {
            manager.Startup();
        }

        yield return null;

        int numberOfManagers = _managers.Count;
        int numberOfRunningManagers = 0;

        while (numberOfRunningManagers < numberOfManagers)
        {
            int lastRunningManager = numberOfRunningManagers;

            foreach (IGameManager manager in _managers)
            {
                if (manager.ManagerStatus == ManagerStatus.Started)
                {
                    numberOfRunningManagers++;
                }
            }

            if (numberOfRunningManagers > lastRunningManager)
            {
                Debug.Log($"Progress: {numberOfRunningManagers}/{numberOfManagers}");
                yield return null;
            }
        }

        Debug.Log("All managers started up");
    }
}
