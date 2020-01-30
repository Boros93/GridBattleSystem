using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public string tileName;
    public GameObject tilePrefab;
    // Set for special tile
    public float movementCost = 1;
    public bool isWalkable = true;
}
