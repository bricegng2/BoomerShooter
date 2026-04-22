using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List <KeyData> keys = new List<KeyData>();

    public List<PlayerGun> guns = new List<PlayerGun>();

    // move the ammo stuff into here later

    public int throwingKnives = 100;
}
