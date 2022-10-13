using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject InstallUI;//ªÒ»°bottomUI
    public Animator UIAnim;
    private float duration = 1.4f;
    public void WaitforAnim()
    {
        UIAnim.SetBool("Idle", false);
        UIAnim.SetBool("Attack", true);
        Invoke(nameof(GameStart), duration);
    }

   private void GameStart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
   

    public GameObject installPane;

    public void Install()
    {
        installPane.SetActive(true);
    }

    public void InstallFalse()
    {
        installPane.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {

        }
    }
}
