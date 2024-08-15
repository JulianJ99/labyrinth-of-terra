using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownManager : MonoBehaviour
{
    [Header("Public references")]
    public TMP_Text currentLocation;
    public Image currentImage;
    public Sprite[] backgroundSprites;
    public GameObject[] connectedUI;
    public string[] locationNames;

    private string currentPage;
    private int currentNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        currentNumber = 0;

        currentLocation.text = locationNames[currentNumber];
        currentImage.sprite = backgroundSprites[currentNumber];

        for (int i = 0; i < connectedUI.Length; i++)
        {
            connectedUI[i].gameObject.SetActive(false);
        }

        connectedUI[currentNumber].gameObject.SetActive(true);
    }

    public void NextPage()
    {
        currentNumber++;

        if (currentNumber > connectedUI.Length -1)
        {
            currentNumber = connectedUI.Length -1;
        }

        currentLocation.text = locationNames[currentNumber];
        currentImage.sprite = backgroundSprites[currentNumber];

        for(int i = 0; i < connectedUI.Length; i++)
        {
            connectedUI[i].gameObject.SetActive(false);
        }

        connectedUI[currentNumber].gameObject.SetActive(true);
    }

    public void PrevPage()
    {
        currentNumber--;

        if (currentNumber < 0)
        {
            currentNumber = 0;
        }

        currentLocation.text = locationNames[currentNumber];
        currentImage.sprite = backgroundSprites[currentNumber];

        for (int i = 0; i < connectedUI.Length; i++)
        {
            connectedUI[i].gameObject.SetActive(false);
        }

        connectedUI[currentNumber].gameObject.SetActive(true);
    }
}
