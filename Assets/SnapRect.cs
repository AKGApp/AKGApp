using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapRect : MonoBehaviour
{
    public Transform content;
    public Scrollbar scrollbar;
    public Transform pagniation;
    public Button nextButton;
    public Button backButton;
    public Sprite selectedScreen;
    public Sprite unselectedScreen;
    public float[] pos;
    float distance;
    float oldPos;
    int currentPos;
    Transform pagniationContent;

    // Start is called before the first frame update
    void Start()
    {
        distance = 1f / (content.childCount - 1);
        pos = new float[content.childCount];
        pagniationContent = pagniation.GetChild(0);

        for (int i = 0; i < content.childCount; i++)
        {
            pos[i] = distance * i;
            GameObject img = new GameObject();
            img.AddComponent<Image>();
            img.transform.localScale = Vector3.one;
            img.transform.SetParent(pagniationContent);
        }

        nextButton.onClick.AddListener(() => { StartCoroutine(NextScreen(1)); });
        backButton.onClick.AddListener(() => { StartCoroutine(BackScreen(1)); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            oldPos = scrollbar.value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (oldPos < pos[i] + (distance / 2) && oldPos > pos[i] - (distance / 2))
                {
                    pagniationContent.GetChild(i).GetComponent<Image>().sprite = selectedScreen;
                    scrollbar.value = Mathf.Lerp(scrollbar.value, pos[i], 0.3f);
                    currentPos = i;
                }
                else
                {
                    pagniationContent.GetChild(i).GetComponent<Image>().sprite = unselectedScreen;
                }
            }
        }
    }
    /// <summary>
    /// Method that will allow for next screen navigation, this method will automatically not go beyond the number of contents in the pagniation solution
    /// Will need to update this to take only page navigation instead of next and later back
    /// </summary>
    /// <param name="newPos">provide the number of the sceen in the order you want, i.e. first screen is 1</param>
    /// <returns></returns>
    IEnumerator NextScreen(float newPos)
    {
        // * This will change later to accomodate a more flexible process  
        while (scrollbar.value < pos[currentPos + 1] - 0.2f)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, pos[currentPos + 1], 0.3f);

        }
        oldPos = scrollbar.value;
        yield return null;
    }
    IEnumerator BackScreen(float newPos)
    {
        // * This will change later to accomodate a more flexible process  
        while (scrollbar.value > pos[currentPos - 1] + 0.2f)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, pos[currentPos - 1], 0.3f);

        }
        oldPos = scrollbar.value;
        yield return null;
    }
}
