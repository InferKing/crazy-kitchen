using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ CreateAssetMenu(fileName = "Loan", menuName = "Loan")]
public class Loan : ScriptableObject
{
    // Кредит должен зависеть от уровня ресторана
    [field: SerializeField, MinValue(1)] public float Amount { get; private set; }
    [field: SerializeField, MinValue(1), MaxValue(60)] public int Period { get; private set; }
    [field: SerializeField, MinValue(5), MaxValue(100)] public float Rate { get; private set; }
    [field: SerializeField, MinValue(0), MaxValue(100)] public float MinRestaurantReputation { get; private set; }
    [field: SerializeField] public Difficulty Difficulty { get; private set; }
    public float TotalPayments => Amount * (100 + Rate);
    public float PeriodPayment => TotalPayments / Period;
    public float RemainingPayments => TotalPayments - PeriodPayment * _daysPaid;
    public bool IsPaidOff => _daysPaid >= Period;
    private int _daysPaid = 0;
    private int _skipDays = 0;
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
