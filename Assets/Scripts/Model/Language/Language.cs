 using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Language", order = 51)]
public class Language : ScriptableObject
{
    [field: SerializeField] public LanguageType Current { get; private set; }
}
