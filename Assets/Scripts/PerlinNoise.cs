using UnityEngine;

public class PerlinNoise
{
    private float[] noise;
    private int xSize, zSize;

    public PerlinNoise(int xSize, int zSize, int density)
    {
        noise = new float[(xSize + 1) * (zSize + 1)];
        this.xSize = xSize;
        this.zSize = zSize;

        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float xCoord = (float)x / xSize * density;
                float zCoord = (float)z / zSize * density;
                noise[i] = Mathf.PerlinNoise(xCoord, zCoord);
            }
        }
    }

    public float this[int i, int j]
    {
        get
        {
            if (i > xSize)
            {
                return -1;
            }
            if (j > zSize)
            {
                return -1;
            }

            int row = (xSize + 1) * j;
            return noise[i + row];
        }
    }
}