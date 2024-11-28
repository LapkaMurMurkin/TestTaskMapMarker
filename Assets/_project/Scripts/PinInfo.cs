using System;
using TMPro;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class PinInfo
{
    public string PlaceName;
    public string Description;
    public Texture2D Photo;
    public Vector2 Coords;

    public PinInfo()
    {
        PlaceName = "Empty";
        Description = "Empty";
        Photo = new Texture2D(2, 2);
        Coords = Vector2.zero;
    }
}
