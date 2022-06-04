using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberValueControler : MonoBehaviour
{
    public int maxNumberValue;
    public int minNumberValue;
    public bool allowNumberConstraints;
    public int defualtValue = 0;
    public InputField valueField;
    public Button incrementButton;
    public Button decreamentButton;
    // Start is called before the first frame update
    void Start()
    {
        valueField.text = defualtValue.ToString();
        incrementButton.onClick.AddListener(() => { IncreaseValue(); });
        decreamentButton.onClick.AddListener(() => { DecreaseValue(); });
    }

    void IncreaseValue()
    {
        if (allowNumberConstraints)
        {
            if (defualtValue < maxNumberValue)
            { defualtValue += 1; }
        }
        else
        {
            defualtValue += 1;
        }
        valueField.text = defualtValue.ToString();
    }
    void DecreaseValue()
    {
        if (allowNumberConstraints)
        {
            if (defualtValue > minNumberValue)
            { defualtValue -= 1; }
        }
        else
        {
            defualtValue -= 1;
        }
        valueField.text = defualtValue.ToString();
    }
}
