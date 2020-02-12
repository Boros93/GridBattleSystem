using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new battle", menuName = "Combat System/Battle")]
public class BattleSetup : ScriptableObject
{
    public Vector3 playerStartPosition;
    public Vector3[] enemiesStartPosition;
}
