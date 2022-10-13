using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTool : MonoBehaviour
{
    public GameObject Boss;
    public Transform BossUI;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckEvent(Boss);
        WizHealth.InitializationUI(prefab, BossUI);
        Destroy(gameObject);
    }

    public static  void CheckEvent(GameObject curObject)
    {
        curObject.SetActive(true);
    }
}
