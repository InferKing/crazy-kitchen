using UnityEngine;

public class Interactable : BaseInteractable
{
    private EventBus _bus;
    public EventBus Bus { get { return _bus; } }
    [field: SerializeField] public string ObjectName { get; private set; } = "Unnamed";
    [Tooltip("Put some transform if you want to replace basic gameobject transform for fx")]
    [field: SerializeField] public Transform TransformToFX {  get; private set; }
    protected virtual void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        if (TransformToFX == null)
        {
            TransformToFX = gameObject.transform;
        }
    }
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        if (gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = true;
        }
        else
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 5;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }
    public override void OnExit()
    {
        if (gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }
    }
    public virtual bool TryCombine(Interactable item, out bool stayInHand)
    {
        stayInHand = false;
        return false;
    }
}
