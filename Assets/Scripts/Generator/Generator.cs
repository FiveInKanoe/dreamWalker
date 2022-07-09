using UnityEngine;

public abstract class Generator : ScriptableObject
{
    public char[,] CharMap { get; protected set; }
    public int Width { get; protected set; }
    public int Height { get; protected set; }

    private void OnEnable()
    {
        CharMap = null;
    }

    public abstract void InitMap(char[,] charMap, bool isPreGen);

    public abstract void Generate();
}
