using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject objectToPool;
        public int maxAmountToPool;
        public bool shouldExpand;
    }
    
    public PoolItem itemToPool;
    public List<GameObject> pooledObjects;
    
    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        
        for (int i = 0; i < itemToPool.maxAmountToPool; i++)
        {
            GameObject obj = Instantiate(itemToPool.objectToPool, transform, true);
            obj.GetComponent<Pipe>().endPos = GetComponent<PipeHandler>().endPos;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (!itemToPool.shouldExpand) return null;
        
        GameObject obj = Instantiate(itemToPool.objectToPool, transform, true);
        obj.GetComponent<Pipe>().endPos = GetComponent<PipeHandler>().endPos;
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}