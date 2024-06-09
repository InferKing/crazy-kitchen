public class ToggleMovementSignal
{
    public readonly bool data;
    /// <summary>
    /// Set data to true/false if you want to lock/release movement.
    /// </summary>
    public ToggleMovementSignal(bool data)
    {
        this.data = data;
    }
}
