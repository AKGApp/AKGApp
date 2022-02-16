using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//? need to figure ou whu the GameItemStruct struct is not being inititated
// using System.Collections;

public class SpriteScriptInformation : MonoBehaviour
{

    public string resourcesPath = "Component/Icon_ItemIcons(x2)/256/";
    [System.Serializable]
    public struct GameItemsStruct
    {
        private string spriteNumber;
        private string spriteName;
        private string spriteCategory;
        private string spriteType;
        private string spriteColor;
        private string spriteImageLocation;

        // public float spriteSelectItemSize;
        // public float spriteMatchItemSize;
        public GameItemsStruct(string _spriteNumber, string _spriteName, string _spriteCategory, string _sprtieType, string _sprtieColor, string _spriteLocation)
        {
            spriteNumber = _spriteNumber;
            spriteName = _spriteName;
            spriteCategory = _spriteCategory;
            spriteType = _sprtieType;
            spriteColor = _sprtieColor;
            spriteImageLocation = _spriteLocation;
        }
    }
    public GameItemsStruct[] gameItems;

    [SerializeField]
    // public GameItemsStruct[] gameItems;
    private void Start()
    {
        // gameItems[0] = new GameItemsStruct("01", "Hello ", "It ", "Is ", "Me ", "Looking");
        // gameItems[1] = new GameItemsStruct("02", "Hello ", "It ", "Is ", "Me ", "Looking");
    }

}
