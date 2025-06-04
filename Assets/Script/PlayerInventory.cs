using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public int Money { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public bool HasEnoughMoney(int amount)
    {
        return Money >= amount;
    }

    public void SpendMoney(int amount)
    {
        if (HasEnoughMoney(amount))
        {
            Money-=amount;
        }
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }
}
