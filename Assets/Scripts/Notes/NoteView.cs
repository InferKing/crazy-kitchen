using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NoteView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void SetText(string text)
    {
        _text.text = text;
    }
}