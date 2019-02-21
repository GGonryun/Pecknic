using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    [SerializeField] private TextMeshProUGUI pointPrefab;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;


    protected virtual void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        mesh = new Mesh();
        GenerateMesh();
    }

    //protected virtual void OnDrawGizmos()
    //{
    //    if(vertices != null)
    //    {
    //        for(int i = 0; i < vertices.Length; i++)
    //        {
    //            Gizmos.color = Color.black;
    //            Gizmos.DrawSphere(vertices[i], .20f);
    //        }
    //    }
    //}

    private void SetPoints(Vector3[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 point = points[i];
            TextMeshProUGUI sticker = Instantiate(pointPrefab, point, Quaternion.identity, this.transform) as TextMeshProUGUI;
            sticker.text = $"({point.x}, {point.y}, {point.z})";
        }
    }

    private void GenerateMesh()
    {
        int size = (xSize + 1) * (zSize + 1);
        Vector3[] vertices = new Vector3[size];
        int[] triangles = new int[size * 6];

        for (int i = 0, x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++, i++)
            {
                int y = 0;
                vertices[i] = new Vector3(x, y, z);
            }
        }
        SetPoints(vertices);
    }

}
