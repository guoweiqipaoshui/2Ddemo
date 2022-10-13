using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizHealth : MonoBehaviour
{
    public static int maxHealth, currentHealth;
    public Transform BossUI;
    Image fill;
    Text HealthText;
    // Start is called before the first frame update
    void Start()
    {
        fill = BossUI.GetChild(0).GetChild(1).GetComponent<Image>();
        HealthText = BossUI.GetChild(0).GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth(currentHealth, maxHealth);
    }

    void UpdateHealth(int currentHealth,int maxHealth)
    {
        float healthSlider = (float)currentHealth / maxHealth;
        fill.fillAmount = healthSlider;
        HealthText.text = maxHealth.ToString() + "/" + currentHealth.ToString();
    }

    public static void InitializationUI(GameObject prefab,Transform BossUI)
    {
        Instantiate(prefab, BossUI.transform.position,prefab.transform.rotation,BossUI);
    }
}
