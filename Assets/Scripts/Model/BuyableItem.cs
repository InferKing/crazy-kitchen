using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "BuyableItem")]
public class BuyableItem : ScriptableObject
{
    [field: SerializeField] public Interactable Item { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    // Подумать над дальнейшими характеристиками для карточки товара
}
