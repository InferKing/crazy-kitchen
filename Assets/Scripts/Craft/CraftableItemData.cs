using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftableItemData", menuName = "CraftableItemData")]
public class CraftableItemData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public List<CraftItem> Elements { get; private set; }
}
