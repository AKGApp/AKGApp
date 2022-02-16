using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchItemsCollection : MonoBehaviour
{

    [Header("Game Elements")]
    public Component _timerData;
    public GameObject _winUIScreen;
    public GameObject _loseUIScreen;
    public float _distanceToMatch = 10.0f;
    public GameObject _itemToMatchPanel;

    [Header("Game Items")]
    public MatchItItem[] _matchItem;
    public MatchItSlot[] _matchSlots;

    private Vector2 _selectedMatchItemDroppedLocation;
    public void DroppedItem()
    {
        Debug.Log("Plop");
        foreach (MatchItItem i in _matchItem)
        {

            foreach (MatchItSlot s in _matchSlots)
            {
                // Debug.Log(i.GetComponent<MatchItInformation>().itemName.ToString());

                if (i.GetComponent<MatchItInformation>().itemName.ToString() == s.GetComponent<MatchItInformation>().itemName.ToString())
                {
                    float _distance = Vector3.Distance(i.transform.position, s.transform.position);
                    Debug.Log(i.transform.position);
                    Debug.Log(s.transform.position);
                    Debug.Log(_distance);
                    if (_distance <= _distanceToMatch)
                    {
                        Debug.Log("Matched");
                        //TODO: Play win sound
                        s.GetComponent<Image>().sprite = i.GetComponent<Image>().sprite;
                        i._isMatched = true;
                        s._isMatched = true;
                    }
                }
            }
        }
        if (Array.TrueForAll(_matchSlots, CheckIfAllItemsMatched))
        {
            Invoke("EnableWinningScreen", 1.0f);   
        }
    }

    private static bool CheckIfAllItemsMatched(MatchItSlot value)
    {
        return value._isMatched;
        throw new NotImplementedException();
    }
    private void EnableWinningScreen()
    {
        _winUIScreen.SetActive(true);
    }
}