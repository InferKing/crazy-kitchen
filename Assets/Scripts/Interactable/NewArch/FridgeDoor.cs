using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : Door
{
    [SerializeField] private PulloutShelf[] _side;
    public override void Interact()
    {
        foreach (var item in _side)
        {
            if (item.GetStateName() == _open)
            {
                return;
            }
        }
        _fsm.OnLogic();
    }
}
