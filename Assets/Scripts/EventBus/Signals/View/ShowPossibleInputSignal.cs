using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPossibleInputSignal
{
    public readonly IReadOnlyDictionary<KeyCode, System.Action> data;
    public ShowPossibleInputSignal(IReadOnlyDictionary<KeyCode, System.Action> data)
    {
        this.data = data;
    }
}
