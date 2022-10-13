using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

   
    [SerializeField] Pool[] monsterPools;
    [SerializeField] Pool[] bulletPools;
    [SerializeField] Pool[] FlyEyePools;
    [SerializeField] Pool[] MagicCircle;

    static Dictionary<GameObject, Pool> dictionary;

    void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(monsterPools) ;
        Initialize(bulletPools);
        Initialize(FlyEyePools);
        Initialize(MagicCircle);
    }

    /*void CheckPoolSize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            if(pool.RunTimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("RunTimeSize is bigger than Size" ,pool.Prefab.name,pool.RunTimeSize,pool.Size));
            }
        }
    }*/
    
    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
        #if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("already have the key inside the pool  " + pool.Prefab.name);
                continue;
            }
        #endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("pool:" + pool.Prefab.name).transform;

            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }

    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool has no prefab inside  " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject();
    }

    public static GameObject Release(GameObject prefab,Vector2 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool has no prefab inside  " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject(position);
    }
    public static GameObject Release(GameObject prefab, Vector2 position, Vector2 localScale, float speed)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool has no prefab inside  " + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PrepareObject(position,localScale,speed);
    }
}
