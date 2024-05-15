public class PlayFXSignal
{
    public readonly UnityEngine.Transform transform;
    public readonly FXType type;
    public readonly UnityEngine.Transform lookAt;
    public PlayFXSignal(UnityEngine.Transform transform, FXType type, UnityEngine.Transform lookAt = null)
    {
        this.transform = transform;
        this.type = type;
        this.lookAt = lookAt;
    }
}
