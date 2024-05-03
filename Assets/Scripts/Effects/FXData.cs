using UnityEngine;

[CreateAssetMenu(fileName = "FX", menuName = "FX", order = 51)]
public class FXData : ScriptableObject
{
    [field: SerializeField] public FXType Type { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
