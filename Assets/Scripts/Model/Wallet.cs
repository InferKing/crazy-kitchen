[System.Serializable]
public class Wallet
{
    [UnityEngine.SerializeField] private float _curBalance;
    public bool TryPurchase(float money)
    {
        if (_curBalance < money)
        {
            return false;
        }
        _curBalance -= money;
        return true;
    }
    public void AddMoney(float money)
    {
        if (money <= 0) return;
        _curBalance += money;
    }
    public float Balance => _curBalance;
}
