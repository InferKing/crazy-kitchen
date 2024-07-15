using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Loan : ScriptableObject
{
    // Кредит должен зависеть от уровня ресторана
    [field: SerializeField, Min(1)] public float Amount { get; private set; }
    [field: SerializeField, MinMaxSlider(1, 60)] public int Period { get; private set; }
    [field: SerializeField, MinMaxSlider(5, 100)] public float Rate { get; private set; }
    public float TotalPayments => Amount * (100 + Rate);
    public float PeriodPayment => TotalPayments / Period;
    public float RemainingPayments => TotalPayments - PeriodPayment * _daysPaid;
    public bool IsPaidOff => _daysPaid >= Period;
    private int _daysPaid = 0;
    private int _skipDays = 0;
    private void OnValidate()
    {
        // 10000
        // 17 дней
        // 25% на срок
    }
    public bool TryPayment(Wallet wallet)
    {
        bool flag = wallet.TryPurchase(PeriodPayment);
        if (flag)
        {
            _daysPaid++;
        }
        else
        {
            _skipDays++;
        }
        return flag;
    }
}
