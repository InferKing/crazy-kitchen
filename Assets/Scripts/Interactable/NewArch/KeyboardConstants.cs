using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardConstants
{
    public static IReadOnlyDictionary<char, KeyCode> KeyCodeMatch = new Dictionary<char, KeyCode>()
    {
        { 'a', KeyCode.A },
        { 'b', KeyCode.B },
        { 'c', KeyCode.C },
        { 'd', KeyCode.D },
        { 'e', KeyCode.E },
        { 'f', KeyCode.F },
        { 'g', KeyCode.G },
        { 'h', KeyCode.H },
        { 'i', KeyCode.I },
        { 'j', KeyCode.J },
        { 'k', KeyCode.K },
        { 'l', KeyCode.L },
        { 'm', KeyCode.M },
        { 'n', KeyCode.N },
        { 'o', KeyCode.O },
        { 'p', KeyCode.P },
        { 'q', KeyCode.Q },
        { 'r', KeyCode.R },
        { 's', KeyCode.S },
        { 't', KeyCode.T },
        { 'u', KeyCode.U },
        { 'v', KeyCode.V },
        { 'w', KeyCode.W },
        { 'x', KeyCode.X },
        { 'y', KeyCode.Y },
        { 'z', KeyCode.Z },
    };
    public static IReadOnlyDictionary<KeyCode, string> KeysDescription = new Dictionary<KeyCode, string>() 
    {
        { KeyCode.G, Constants.keyGInput },
        { KeyCode.F, Constants.keyFInput },
        { KeyCode.Mouse1, Constants.keyMouse1Input },
        { KeyCode.E, Constants.keyEInput },
    };
}
