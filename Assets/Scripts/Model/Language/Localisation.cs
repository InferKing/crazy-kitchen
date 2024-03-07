using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// The Localisation class loads information about all localizations at startup
/// </summary>
public class Localisation
{
    private Dictionary<LanguageType, Dictionary<string, string>> _translations;
    private Dictionary<string, string> _curTranslation;
    public Localisation(LanguageType type) 
    {
        Debug.Log($"Current data path: {Application.dataPath}");
        string json = File.ReadAllText(Application.dataPath);
        // _translations = JsonUtility.FromJson<Dictionary<LanguageType, Dictionary<string, string>>>(json);
    }
}
