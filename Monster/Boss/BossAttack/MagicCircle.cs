using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    bool isIn = false;
    public GameObject player;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            isIn = true;
            player = coll.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            isIn = false;
        }
    }
    void HitPlayer()
    {
        if (isIn)
            player.GetComponent<PlayerControl>().hurtSet = true;
        gameObject.SetActive(false);
    }
}
