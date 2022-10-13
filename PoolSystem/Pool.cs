using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable] public class Pool
{
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    public int Size => size;
    //public int RunTimeSize => PoolQ.Count;

    [SerializeField]
    GameObject prefab;
    public int size = 1;

    Queue<GameObject> PoolQ;

    Transform parent;

    public void Initialize(Transform parent)
    {
        PoolQ = new Queue<GameObject>();
        this.parent = parent;

        for(int i = 0; i < size; i++)
        {
            PoolQ.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;

        if (PoolQ.Count > 1 && !PoolQ.Peek().activeSelf)
        {
            availableObject = PoolQ.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }

        PoolQ.Enqueue(availableObject);

        return availableObject;
    }

    public GameObject PrepareObject()
    {
        GameObject prepareObject = AvailableObject();
        prepareObject.SetActive(true);

        return prepareObject;
    }

    public GameObject PrepareObject(Vector2 position)
    {
        GameObject prepareObject = AvailableObject();
        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        return prepareObject;
    }

    public GameObject PrepareObject(Vector2 position,Vector2 localScale,float speed)
    {
        GameObject prepareObject = AvailableObject();
        prepareObject.SetActive(true);
        prepareObject.transform.position = position;
        prepareObject.transform.localScale = localScale;
        prepareObject.GetComponent<MagicBullet>().SetSpeed(speed);

        return prepareObject;
    }
}
