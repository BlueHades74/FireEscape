using System;
using UnityEngine;

public class CharacterEvents
{
    public static Action<int, CharacterData> PlayerCharLoad;
    public static Action<char, string> PlayerSharedKeyPress;
}
