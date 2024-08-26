using System.Collections.Generic;
using UnityEngine;

public class Color : Singleton<Color>
{
    public enum color 
    {
        Black,
        Blue,
        Brown,
        GrayGreen,
        Green,
        Pink,
        Purple,
        Red,
        White,
        Yellow
    }

    public List<Material>materials = new List<Material>();

    public Material SetMaterialInGame(int colorIndex) 
    {
        return materials[colorIndex];
    }
}
