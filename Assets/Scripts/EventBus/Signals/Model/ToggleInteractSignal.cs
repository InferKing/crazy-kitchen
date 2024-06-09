public class ToggleInteractSignal
{
    public readonly bool data;
    /// <summary>
    /// Set data to true/false if you want to lock/release interact
    /// </summary>
    public ToggleInteractSignal(bool data)
    {
        this.data = data;
    }
}
