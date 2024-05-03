public class PlayFXSignal
{
    public readonly UnityEngine.Transform transform;
    public readonly FXType type;
    public PlayFXSignal(UnityEngine.Transform transform, FXType type)
    {
        this.transform = transform;
        this.type = type;
    }
}
