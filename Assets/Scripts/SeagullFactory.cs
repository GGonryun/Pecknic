using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andtech;


[CreateAssetMenu(fileName = "New Seagull Factory", menuName = "Factories/Seagull Factory")]
public class SeagullFactory : ScriptableObject
{
    public Seagull template;
    private Stack<Seagull> objectPool;

    public Seagull Get()
    {

        if (objectPool == null)
        {
            objectPool = new Stack<Seagull>();
        }

        if (objectPool.Count > 0)
        {
            return objectPool.Pop();
        }
        else
        {
            return Instantiate(template) as Seagull;
        }
    }

    public void Recycle(Seagull obj)
    {
        objectPool.Push(obj);
        obj.gameObject.SetActive(false);
    }
}
