using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFactory<T> : ScriptableObject where T : MonoBehaviour
{
    public T[] templates;
    private List<T> objectPool;

    public virtual void Initialize()
    {
        objectPool = new List<T>();
    }

    public virtual T Get()
    {

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
        int objIndex = Random.Range(0, templates.Length);
        T newObj = Instantiate(templates[objIndex]);
        return newObj;
    }

    public virtual void Recycle(T obj)
    {
        objectPool.Add(obj);
        obj.gameObject.SetActive(false);
    }

}
