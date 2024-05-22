using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleResources : MonoBehaviour
{
    public TMP_Text randAmountText;
    public TMP_Text waterAmountText;
    public TMP_Text electricityAmountText;
    public int rand = 10;
    public int water = 10;
    public int electricity = 10;

    private float timer = 0f;
    public float delayAmount = 10f;

    void Update()
    {
        UpdateRandText();
        UpdateElectricityText();
        UpdateWaterText();
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            rand++;
            water++;
            electricity++;
            Debug.Log(rand);
            Debug.Log(water);
            Debug.Log(electricity);
        }
    }
    private void UpdateRandText()
    {
        if (randAmountText != null)
        {
            randAmountText.text = rand.ToString();
            
        }
    }
    private void UpdateElectricityText()
    {
        if (waterAmountText != null)
        {
            waterAmountText.text = water.ToString();
            
        }
    }
    private void UpdateWaterText()
    {
        if (electricityAmountText != null)
        {
            electricityAmountText.text = electricity.ToString();
            
        }
    }
}

