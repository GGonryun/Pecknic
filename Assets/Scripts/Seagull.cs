using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour, IDespawnable
{


    void IDespawnable.Despawn()
    {
        SeagullSpawner.Current.Despawn(this);
    }
}
