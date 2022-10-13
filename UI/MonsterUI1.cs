using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI1 : MonoBehaviour
{
    public GameObject healthBar;
    GameObject localHealthBar;
    public Transform healthBarPos;
    public Transform canvas;
    int currentHealth,maxHealth;

    Image fill;  
    
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        localHealthBar = Instantiate(healthBar, canvas);
        healthBarPos = localHealthBar.transform;

        fill = healthBarPos.GetChild(2).GetComponent<Image>();
   
    }

    void Update()
    {
        healthBarPos.position = GetComponent<FSM>().parameter.healthBarPoint.transform.position;
        UpdateHealth(currentHealth = GetComponent<FSM>().parameter.currentHealth, maxHealth = GetComponent<FSM>().parameter.maxHealth);
        if(currentHealth > 0)
            localHealthBar.SetActive(true);
        else
            localHealthBar.SetActive(false);
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }

        float healthSlider = (float)currentHealth / maxHealth;
        fill.fillAmount = healthSlider;
    }
}
