using UnityEngine;

public enum EKeyType
{
    Red,
    Yellow,
    Blue,
}

[CreateAssetMenu(fileName = "New Key", menuName = "Scriptable Objects/Key")]
public class KeyData : ScriptableObject
{
    public EKeyType keyType;

    public Material keyMaterial;
}
