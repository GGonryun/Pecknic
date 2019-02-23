using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> : ScriptableObject where T : MonoBehaviour
{
    public T template;
    protected Stack<T> objectPool;


    public virtual T Get()
    {

        if (objectPool == null)
        {
            objectPool = new Stack<T>();
        }

        if (objectPool.Count > 0)
        {
            return objectPool.Pop();
        }
        else
        {
            return Instantiate(template) as T;
        }
    }


    public virtual void Recycle(T obj)
    {
        objectPool.Push(obj);
        obj.gameObject.SetActive(false);
    }

}
