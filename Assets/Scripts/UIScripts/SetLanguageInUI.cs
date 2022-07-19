using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguageInUI : MonoBehaviour
{
    public enum language { Arabic, English };

    public language _selectedLangauge = (language)MVP_UIManager.instance.systemLanguage;

    public void SetUILanguage(language gameLanguage)
    {
        DisableAllScreens();
        switch (gameLanguage)
        {
            case language.Arabic:
                {
                    this.transform.Find("ArabicScene").gameObject.SetActive(true);
                    break;
                }
            case language.English:
                {
                    this.transform.Find("EnglishScene").gameObject.SetActive(true);
                    break;
                }
        }
    }
    /// <summary>
    /// DIsables all language scenes
    /// </summary>
    private void DisableAllScreens()
    {
        this.transform.Find("ArabicScene").gameObject.SetActive(true);
        this.transform.Find("EnglishScene").gameObject.SetActive(true);

    }


}
