public class ToggleRotationSignal
{
    public readonly bool data;
    /// <summary>
    /// Set data to true/false if you want to lock/release camera rotation.
    /// </summary>
    public ToggleRotationSignal(bool data)
    {
        this.data = data;
    }
}
