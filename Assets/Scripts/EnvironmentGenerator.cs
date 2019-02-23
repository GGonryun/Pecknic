using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private EnvironmentFactory factory;
    [Range(50, 200)] [SerializeField] private float growthRate = 100;
    [Range(0, .499f)] [SerializeField] private float offset = 0.05f;


    public List<GameObject> Initialize(int xSize, int zSize, int scale, PerlinNoise heightMap, PerlinNoise environmentSpawnLayout)
    {
        List<GameObject> environment = new List<GameObject>();
        float grow = 0;

        int xMin = (int)Mathf.Floor(xSize * offset);
        int zMin = (int)Mathf.Floor(zSize * offset);
        int xMax = xSize - xMin;
        int zMax = zSize - zMin;

        for (int z = zMin; z < zMax; z++)
        {
            for (int x = xMin; x < xMax; x++)
            {
                grow += environmentSpawnLayout[x, z];
                if (grow > growthRate)
                {
                    grow -= growthRate;
                    GameObject obj = factory.Get();
                    obj.transform.localPosition = new Vector3(x, (heightMap[x, z] * scale * .90f), z);
                    environment.Add(obj);
                }
            }
        }
        return environment;
    }

}
