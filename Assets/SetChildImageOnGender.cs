using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetChildImageOnGender : MonoBehaviour
{
    public Image selectedImage;
    public Sprite setSprite;

    public void SetNewImage()
    { 
        selectedImage.sprite = setSprite;
    }
}
