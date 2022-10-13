using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInsideUI : MonoBehaviour
{
    public GameObject GOverPanel;
    public static bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == true)
        {
            GOverPanel.SetActive(true);
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GOverPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
