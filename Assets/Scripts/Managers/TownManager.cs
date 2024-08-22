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
    public Sprite startingSprite;
    public GameObject[] connectedLocations;
    public string[] locationNames;

    private int startingLocation = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < connectedLocations.Length; i++)
        {
            connectedLocations[i].gameObject.SetActive(false);
        }

        connectedLocations[startingLocation].gameObject.SetActive(true);
        currentLocation.text = locationNames[startingLocation];
        currentImage.sprite = startingSprite;
    }

    public void LoadPage(string locName)
    {
        for(int i = 0;i < connectedLocations.Length;i++)
        {
           UiInformation currentUI = connectedLocations[i].GetComponent<UiInformation>();
           if(locName == currentUI.identifier)
           {
              currentImage.sprite = currentUI.connectedSprite;
              currentLocation.text = currentUI.identifier;

              ResetActivePage();
              connectedLocations[currentUI.locationNumber].gameObject.SetActive(true);
           }
        }
    }

    private void ResetActivePage()
    {
        for (int i = 0; i < connectedLocations.Length; i++)
        {
            connectedLocations[i].gameObject.SetActive(false);
        }
    }
}
