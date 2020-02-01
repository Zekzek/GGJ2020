using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField]
    private string type;
    public string Type { get => type; }

    [SerializeField]
    private int currentHealth = 100;
    public int CurrentHealth { get => currentHealth; }

    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth { get => maxHealth; }

    public void Damage(int amount)
    {
        currentHealth = Math.Max(0, currentHealth - amount);
    }

    public void Fix(int amount)
    {
        currentHealth = Math.Min(maxHealth, currentHealth + amount);
    }
}
