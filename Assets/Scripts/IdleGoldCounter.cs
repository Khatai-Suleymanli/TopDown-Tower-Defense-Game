using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdleGoldCounter : MonoBehaviour
{
    [Header("------Settings-------")]
    private float increasePerSecond = 1f;
    private float maxAmount = 10.0f;
    private float turretCost = 2.0f;


    [Header("------UI-------")]
    public TMP_Text countertext;


    [Header("------Color-------")]
    private Color normalColor = Color.red;
    private Color approachingMaxColor = Color.yellow;
    private Color maxColor = Color.green;



    private float currentValue = 0.0f;
    private bool isAtMax = false;
    public bool CanAffordTurret()
    {
        return currentValue >= turretCost;
    }
    public void buyTurrette()
    {
        if (CanAffordTurret())
        {
            currentValue -= turretCost;
            UpdateCounterText();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentValue < maxAmount)
        {
            currentValue += increasePerSecond * Time.deltaTime;
            currentValue = Mathf.Min(currentValue, maxAmount);
            countertext.text = currentValue.ToString();
            UpdateCounterText();
        }
    }
    private void UpdateCounterText()
    {
        countertext.text = currentValue.ToString("F1");
    }


    /*private void UpdateUI() {
        if (currentValue>= maxAmount || isAtMax) {
            countertext.text = currentValue.ToString();
            countertext.color = maxColor;

        }

        if (!isAtMax && currentValue > 5)
        {
            countertext.text = currentValue.ToString();
            countertext.color = approachingMaxColor;
            

        }
        else
        {
            countertext.text = currentValue.ToString();
            countertext.color = normalColor;
        }
    
    }*/
}
