using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Я долбоеб, потому что слил логику и UI!
public class ShopCard : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name, _price, _count, _description;
    private BuyableItem _item;
    public int Amount { get; private set; } = 0;
    public BuyableItem Item
    {
        get { return _item; }
        set { _item = value; }
    }
    public void UpdateUI()
    {
        _image.sprite = Item.Sprite;
        _name.text = Item.Name;
        _price.text = string.Format("${0:f2}", Item.Price);
        if (_description != null)
        {
            _description.text = Item.Description;
        }
    }
    public void Add()
    {
        Amount = ClampAmount(++Amount);
        SetText(Amount.ToString());
    }
    public void Decrease()
    {
        Amount = ClampAmount(--Amount);
        SetText(Amount.ToString());
    }
    private int ClampAmount(int amount) => Mathf.Clamp(amount, 0, 20);
    private void SetText(string text) => _count.text = text;
}
