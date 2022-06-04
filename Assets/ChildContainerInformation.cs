using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChildContainerInformation : MonoBehaviour
{
    public Image deleteChild;
    public Image childImage;
    public Image addChild;
    public Text childName;

    public bool enbaleChildImage;
    public bool enableAddChild;
    public Sprite setChildImage;
    public string SetChildName;
    // Start is called before the first frame update
    void Awake()
    {
        deleteChild.enabled = enbaleChildImage;
        childImage.enabled = enbaleChildImage;
        addChild.enabled = enableAddChild;
        childName.text = SetChildName;
        childImage.sprite = setChildImage;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
