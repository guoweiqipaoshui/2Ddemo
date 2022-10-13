using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minmap : MonoBehaviour
{
    public Transform playerTran;
    float playerX;
    float playerY;

    private void Start()
    {
        playerX = playerTran.position.x;
        playerY = playerTran.position.y;
    }

    void Update()
    {
        if(playerX != playerTran.position.x)
        {
            transform.position = new Vector3(transform.position.x + (playerTran.position.x - playerX) / 2, transform.position.y);
            playerX = playerTran.position.x;
        }
        if (playerY != playerTran.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (playerTran.position.y - playerY) / 2);
            playerY = playerTran.position.y;
        }
    }
}