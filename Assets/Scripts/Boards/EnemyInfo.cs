using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyInfo", menuName = "cardRogue/EnemyInfo", order = 0)]
public class EnemyInfo: ScriptableObject
{
    public CreatureInfo left, center, right;
}