using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TerrainGenerator terrain;

    public void Start()
    {
        terrain = Instantiate(terrain) as TerrainGenerator;
    }

}
