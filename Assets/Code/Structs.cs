using UnityEngine;

public struct PlaneMovement
{
    public Vector3 Forward;
    public Vector3 Backward;
    public Vector3 Right;
    public Vector3 Left;
}
public struct TouchInfo
{
    public bool Pressed;
    public Vector2 Direction { get => (CurrentPos - InitPos).normalized; }
    public Vector2 InitPos;
    public Vector2 CurrentPos;
}
