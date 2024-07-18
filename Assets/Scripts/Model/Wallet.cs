[System.Serializable]
public class Wallet
{
    [field: UnityEngine.SerializeField] public float Balance { get; private set; }
    private float _balance;
    public Wallet() 
    { 
        _balance = Balance;
    }
    public Wallet(float balance)
    {
        _balance = balance;
    }
    public bool TryPurchase(float money)
    {
        if (_balance < money)
        {
            return false;
        }
        _balance -= money;
        return true;
    }
    public void AddMoney(float money)
    {
        if (money <= 0) return;
        _balance += money;
    }
}
