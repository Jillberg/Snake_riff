using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI relicsText;
    private int relicCounter=0;
    private int totalRelicCount;
    private int currentRelicCount;
    // Start is called before the first frame update
    void Start()
    {
        totalRelicCount = GameObject.FindGameObjectsWithTag("Food").Length;
        currentRelicCount=totalRelicCount;
        relicsText.text += relicCounter.ToString() + " / " + totalRelicCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRelicText();
    }

    void UpdateRelicText()
    {
        if (currentRelicCount > GameObject.FindGameObjectsWithTag("Food").Length)
        {
            relicCounter++;
            relicsText.text ="RELICS: "+ relicCounter.ToString() + " / " + totalRelicCount.ToString();
            currentRelicCount = GameObject.FindGameObjectsWithTag("Food").Length;
        }
    }
}
