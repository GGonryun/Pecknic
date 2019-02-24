[System.Serializable]
public struct VectorRange
{
    public int min, max;
    public static VectorRange zero
    {
        get => new VectorRange(0, 0);
    }
    public VectorRange(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}
