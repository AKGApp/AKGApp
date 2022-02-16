using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenContainerManager : MonoBehaviour
{
    [Header("Container Items")]
    [SerializeField]
    private GameObject[] childInformationCards;
    [SerializeField]
    private Sprite boyChildImage;
    [SerializeField]
    private Sprite girlChildImage;

    private void Awake()
    {
        InitializeChildCards();
        // FirebaseManager.instance.
        EnableCard(childInformationCards[1]);
    }

    //populate prefabs with information form firebase for children
    //manage empty fields
    //be bale to shift towards 
    private void InitializeChildCards()
    {
        foreach (GameObject i in childInformationCards)
        {
            i.transform.Find("ChildInformation").gameObject.SetActive(false);
        }
    }

    private void EnableCard(GameObject ChildContainer)
    {
        ChildContainer.transform.Find("AddTheChildContainer").gameObject.SetActive(false);
        ChildContainer.transform.Find("ChildInformation").gameObject.SetActive(true);
        AddChildInformation(ChildContainer.transform.Find("ChildInformation").gameObject, "Sara", "Daughter");
    }

    private void AddChildInformation(GameObject Child, string childName, string childGender)
    {
        Sprite childImage = boyChildImage;
        if (childGender == "Son")
        {
            childImage = boyChildImage;
        }
        else if(childGender == "Daughter")
        {
            childImage = girlChildImage;
        }
        Child.transform.GetComponentInChildren<Image>().sprite = childImage;
        Child.transform.GetComponentInChildren<Text>().text = childName;
    }
}