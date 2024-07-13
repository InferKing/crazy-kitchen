using UnityEngine;

public class DifficultyConfig : ScriptableObject
{
    [field: SerializeField] public Difficulty Difficulty { get; private set; }
    [field: SerializeField] public Wallet Wallet { get; private set; }

}
