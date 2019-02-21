using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    protected virtual void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = GenerateMesh();
    }

    private Mesh GenerateMesh()
    {
        int totalVertices = (xSize + 1) * (zSize + 1);
        Mesh mesh = new Mesh();
        Vector2[] uv = new Vector2[totalVertices];
        Vector3[] vertices = new Vector3[totalVertices];
        Vector4[] tangents = new Vector4[totalVertices];
        Vector4 tangent = new Vector4(1, 0, 0, -1);
        for (int i = 0, z = 0; z <= xSize; z++)
        {
            for (int x = 0; x <= zSize; x++, i++)
            {
                int y = 0;
                vertices[i] = new Vector3(x, y, z);

                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);

                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        mesh.triangles = GenerateTriangles(totalVertices);
        mesh.RecalculateNormals();
        return mesh;
    }

    private int[] GenerateTriangles(int totalVertices)
    {
        int[] triangles = new int[totalVertices * 6];
        int nextRow = xSize;
        for (int t = 0, v = 0, y = 0; y < zSize; y++, v++)
        {
            for (int x = 0; x < xSize; t += 6, v++, x++)
            {
                triangles[t] = v;
                triangles[t + 2] = triangles[t + 3] = v + 1;
                triangles[t + 1] = triangles[t + 4] = v + nextRow + 1;
                triangles[t + 5] = v + nextRow + 2;
            }
        }
        return triangles;
    }
}