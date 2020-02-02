using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    Station[] station;

    public GameObject winScreen;

    // Start is called before the first frame update

    public void Win()
    {
        winScreen.SetActive(true);

    }
    void Start()
    {
        station = FindObjectsOfType(typeof(Station)) as Station[];
    }

    // Update is called once per frame
    void Update()
    {
        bool allMaxHealth = true;
        foreach (var s in station)
        {
            if (0.9f * s.CurrentHealth < s.MaxHealth)
            {
                allMaxHealth = false;
            }
        }
        if (allMaxHealth)
        {
            Win();
        }
    }

}
