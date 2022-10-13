using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBase : MonoBehaviour
{
    public GameObject goblin;
    public Canvas canvas;
    public SpriteRenderer Base;


    public Transform localPos;
    public Transform BasePoint;
    public float BaseArea;
    bool getTarget;
    Vector2 RandomPoint;
    float posx, posy;

    float timer = 2f;
    public int maxNum = 0;

    void Start()
    {

    }

    void Update()
    {
        if(maxNum < 10 && getTarget)
        GoblinBorn();
    }

    void GoblinBorn()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            RandomPos();
            PoolManager.Release(goblin, RandomPoint);
            timer = 2f;
            maxNum++;
        }
    }

    void RandomPos()
    {
        posx = Random.Range(localPos.position.x + BaseArea,localPos.position.x - BaseArea);
        posy = Random.Range(localPos.position.y + BaseArea, localPos.position.y - BaseArea);
        RandomPoint = new Vector2(posx, posy);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ColorChange(200,50,40);
            getTarget = true;    
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(BasePoint.position, BaseArea);
    }

    void ColorChange(byte r,byte g,byte b)
    {
            Base.color = new Color32(r, g, b,255);
    }
}
