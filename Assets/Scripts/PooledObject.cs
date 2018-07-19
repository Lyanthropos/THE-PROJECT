using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour
{

    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;

    public ObjectPool Pool { get; set; }

    public void ReturnToPool()
    {
        if (Pool)
        {
            Pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T GetPooledInstance<T>(Transform parent) where T : PooledObject
    {
        if (!poolInstanceForPrefab)
        {
            poolInstanceForPrefab = ObjectPool.GetPool(this, parent);
        }
        return (T)poolInstanceForPrefab.GetObject();
    }
}
