using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputDescriptionView : MonoBehaviour
{
    [SerializeField] private TMP_Text _letter, _description;

    public void SetText(string letter, string description)
    {
        _letter.text = letter;
        _description.text = description;
    }
}
