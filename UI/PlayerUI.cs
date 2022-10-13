using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image fill;
    public Text HealthText;
    public static int currentHealth, maxHealth;
    public GameObject Bag;
    public static bool isOpenBag;
    
    void Start()
    {

    }


    void Update()
    {

        UpdateHealth(currentHealth, maxHealth);
        BagBar();
    }

    void UpdateHealth(int currentHealth,int maxHealth)
    {       
        float healthSlider = (float)currentHealth / maxHealth;
        fill.fillAmount = healthSlider;
        HealthText.text = maxHealth.ToString() + "/" + currentHealth.ToString();
    }

    void BagBar()
    {
        Bag.SetActive(isOpenBag);
    }
}
