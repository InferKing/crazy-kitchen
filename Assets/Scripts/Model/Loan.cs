using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Loan : ScriptableObject
{
    [field: SerializeField] public float Amount { get; private set; }
    [field: SerializeField] public int Period { get; private set; }
    [field: SerializeField] public float Rate { get; private set; }
    public float TotalPayments => Amount * Rate;
    private void OnValidate()
    {
        // 10000
        // 17 дней
        // 25% на срок
    }
    public void GetRemainingPayments()
    {

    }
}
