using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Interactable Data", order = 53)]
public class InteractableData : ScriptableObject
{
    [field: SerializeField] public Collider Collider { get; private set; }
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
}
