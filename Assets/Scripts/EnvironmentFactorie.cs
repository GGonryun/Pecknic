using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Environment Factory", menuName = "Factories/Environment Factory")]
public class EnvironmentFactorie : Factory<Environment>
{
    [SerializeField] private GameObject[] environmentObjects;

    public override Environment Get()
    {


        if (environmentObjects != null)
        {
            int objIndex = Random.Range(0, environmentObjects.Length);
            GameObject newObj = Instantiate(environmentObjects[objIndex]);
            return newObj;
        }
        return null;
    }
}
