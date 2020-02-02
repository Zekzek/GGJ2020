﻿using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField]
    private string type;
    public string Type { get => type; }

    [SerializeField]
    private float currentHealth = 100;
    public float CurrentHealth { get => currentHealth; }

    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth { get => maxHealth; }

    public ProgressBar progressBar;

    public float dps = 1;

    public void Update()
    {

        if(currentHealth>0)
        {
            currentHealth -= Time.deltaTime * dps;
        }

        progressBar.current = currentHealth;
        progressBar.maximum = maxHealth;
    }

    public void Damage(int amount)
    {
        currentHealth = Math.Max(0, currentHealth - amount);
    }

    public void Fix(float amount)
    {
        currentHealth = Math.Min(maxHealth, currentHealth + amount);
    }
}
