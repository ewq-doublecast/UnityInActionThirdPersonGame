using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus ManagerStatus { get; private set; }

    [SerializeField]
    private int _currentHealth = 50;

    [SerializeField]
    private int _maxHealth = 100;

    public void Startup()
    {
        Debug.Log("Player manager starting...");
        ManagerStatus = ManagerStatus.Started;
    }

    public void ChangeHealth(int health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        Debug.Log($"Health: {_currentHealth}/{_maxHealth}");
    }
}
