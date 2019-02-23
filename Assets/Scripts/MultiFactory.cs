using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFactory<T> : Factory<T> where T : MonoBehaviour
{
    public T[] templates;
    protected new List<T> objectPool;

    public override T Get()
    {

        if (objectPool == null)
        {
            objectPool = new List<T>();
        }

        if (objectPool.Count > 0)
        {
            return GetRandom();
        }
        else
        {
            return CreateRandom();
        }
    }

    private T GetRandom()
    {
        int objIndex = Random.Range(0, objectPool.Count);
        T obj = objectPool[objIndex];
        objectPool.RemoveAt(objIndex);
        return obj;

    }

    private T CreateRandom()
    {
        int objIndex = Random.Range(0, templates[].Length);
        T newObj = Instantiate(templates[objIndex]);
        return newObj;
    }

    public override void Recycle(T obj)
    {
        objectPool.Add(obj);
        obj.gameObject.SetActive(false);
    }

}
