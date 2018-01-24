using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum poolItems { BalloonBurstEffect0, BalloonBurstEffect1, BalloonBurstEffect2, BalloonBurstEffect3 }; 
public class PoolObject
{
    public GameObject objectPrefab { get; set; }
    public int numberToPool { get; set; }
    public bool isExpandable { get; set; }

    public PoolObject(GameObject objectPrefab, int numberToPool, bool isExpandable)
    {
        this.objectPrefab = objectPrefab;
        this.numberToPool = numberToPool;
        this.isExpandable = isExpandable;
    }

}
public class ObjectPool {
    public List<PoolObject> objectsInPool;
    public List<List<GameObject>> pool;
    public ObjectPool()
    {
        objectsInPool = new List<PoolObject>();
        pool = new List<List<GameObject>>();
    }
    public void addToPool(PoolObject poolObject)
    {
        GameObject parent = new GameObject("Parentof" + objectsInPool.Count);
        objectsInPool.Add(poolObject);
        List<GameObject> tempPool = new List<GameObject>();
        for (int i = 0; i < poolObject.numberToPool; i++)
        {
            GameObject go = MonoBehaviour.Instantiate(poolObject.objectPrefab);
            go.transform.parent = parent.transform;
            poolItems pi = (poolItems)pool.Count;
            go.transform.name = pi.ToString();
            go.SetActive(false);
            tempPool.Add(go);
        }
        pool.Add(tempPool);
    }
}
