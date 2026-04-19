using UnityEngine;

public enum EDoorType
{
    Red,
    Yellow,
    Blue,
}

[CreateAssetMenu(fileName = "New Door", menuName = "Scriptable Objects/Door")]
public class DoorData : ScriptableObject
{
    public EDoorType doorType;

    public Material doorMaterial;
}
