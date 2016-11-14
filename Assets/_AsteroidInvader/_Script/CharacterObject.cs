using UnityEngine;
using System.Collections;
//By @JavierBullrich


/// <summary>Editable class, edit and add all the variables you want after the custom data region. **Do not modify the Deault Data region**</summary>
[CreateAssetMenu(fileName = "Chararacter", menuName = "Blue Square Tools/New Character", order = 1)]
public class CharacterObject : ScriptableObject {

    #region DefaultData

    //Do not modify from here
    [HideInInspector]
    public int id;
    [Header("Default Data")]
    public string visualName;
    public string internalName = "Default";
    public Texture menuTexture;
    public Texture deadTexture;
    public bool canBeRandomBuy = true;
    public int price = 100;

    public enum CharCategory
    {
        REGULAR,
        SQUARE,
        MONSTRUOS
    }

    public CharCategory category;

    public string getCategoryString()
    {
        if (category == CharCategory.REGULAR)
            return "REGULAR";
        else if (category == CharCategory.SQUARE)
            return "SQUARE";
        else if (category == CharCategory.MONSTRUOS)
            return "MONSTRUOS";

        return "";
    }
    //Modify after the endregion
    #endregion

    [Header("Custom Data")]
    public GameObject characterPrefab;

    [Header("Regular skins")]
    public Sprite planet;
    public Sprite deadPlanet;

    [Header("Square skins")]
    public Sprite moon;

    [Header("Monster skins")]
    public Sprite[] asteroid;
    public Sprite[] destroyerAsteroid;
    public Texture background;
    public Sprite boomAsteroid;
    public Sprite coinAsteroid;
    public Sprite frozenAsteroid;
    public Sprite asteroidDebris;
}
