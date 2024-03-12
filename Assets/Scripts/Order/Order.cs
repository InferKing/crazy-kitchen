using UnityEngine;

[CreateAssetMenu(fileName = "Order_Name", menuName = "Order", order = 52)]
public class Order: ScriptableObject
{
    [field: SerializeField] public string DishNameToView { get; private set; }
    [field: SerializeField] public Dish Dish { get; private set; }
    // Блюдо содержит информацию - сколько нужно добавлять продуктов.
    // Блюдо содержит информацию - в каком порядке готовить блюдо.
    // Сначала мясо пожарить, потом соль добавить и т.д.
    // Каждый ингредиент определяется объектом, наследующимся от Interactable
}
