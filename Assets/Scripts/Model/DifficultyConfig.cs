using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty")]
public class DifficultyConfig : ScriptableObject
{
    [field: SerializeField] public Difficulty Difficulty { get; private set; }
    [field: SerializeField] public Wallet Wallet { get; private set; }
    [field: SerializeField] public List<Loan> Loan { get; private set; }
}
