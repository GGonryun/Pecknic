using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andtech;

public class ObjectRecycler : Singleton<ObjectRecycler>
{
    public void Recycle(GameObject obj)
    {
        Destroy(obj);
    }
}
