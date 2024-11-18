using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPCHappinessManager : MonoBehaviour
{
    public Slider happinessSlider; 
    public Image sliderFill; 
    public Image handleImage;
    public float happiness = 100f;

    public float[] happinessThresholds = { 100f, 75f, 50f, 25f, 0f }; 
    public string[] happinessStates = { "Very Happy", "Less Happy", "Straight-Faced", "Unhappy", "Very Angry" }; 
    public Color[] stateColors = { Color.green, Color.yellow, Color.blue, Color.magenta, Color.red }; 
    public Sprite[] handleImages; 

    public float recoverySpeed = 20f;
    public float repairBoost = 15f; 

    private float brokenBuildingsTimer = 0f; 
    public BuildingData[] buildings; 

    private BuildingData.BuildingStatus[] previousStatuses; 

    void Start()
    {
        previousStatuses = buildings.Select(b => b.status).ToArray();
        
        happinessSlider.maxValue = 100f;
        happinessSlider.minValue = 0f;
        
        happinessSlider.value = happiness;
    }

    void Update()
    {
        //Check if any building is broken
        bool anyBroken = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            var building = buildings[i];

            if (building.status == BuildingData.BuildingStatus.Broken)
            {
                building.brokenTimer += Time.deltaTime;
                anyBroken = true;
            }
            else
            {
                building.brokenTimer = 0f; //Reset timer when building is fixed

                // Check if building was repaired
                if (previousStatuses[i] == BuildingData.BuildingStatus.Broken)
                {
                    OnBuildingRepaired();
                }
            }
            
            previousStatuses[i] = building.status;
        }
        
        if (anyBroken)
        {
            brokenBuildingsTimer += Time.deltaTime;
            UpdateHappiness(brokenBuildingsTimer);
        }
        else
        {
            brokenBuildingsTimer = 0f; //Reset timer if no buildings are broken
            GraduallyRecoverHappiness();
        }
        
        happinessSlider.value = happiness; 
        
        UpdateSliderColorAndHandle();
    }

    void UpdateHappiness(float brokenTime)
    {
        if (brokenTime >= 20f && happiness > 75f) happiness = 75f;
        if (brokenTime >= 50f && happiness > 50f) happiness = 50f;
        if (brokenTime >= 80f && happiness > 25f) happiness = 25f;
        if (brokenTime >= 140f) happiness = 0f;
    }

    void GraduallyRecoverHappiness()
    {
        if (happiness < 100f)
        {
            happiness += recoverySpeed * Time.deltaTime;
            happiness = Mathf.Clamp(happiness, 0f, 100f); 
        }
    }

    void UpdateSliderColorAndHandle()
    {
        //Loops through the happiness and apply the correct color/handle image
        for (int i = 0; i < happinessThresholds.Length; i++)
        {
            
            if (happiness >= happinessThresholds[i])
            {
                sliderFill.color = stateColors[i]; 
                handleImage.sprite = handleImages[i]; 
                break;
            }
        }
    }

    void OnBuildingRepaired()
    {
        happiness += repairBoost; //Add boost when building is repaired
        happiness = Mathf.Clamp(happiness, 0f, 100f); 
        Debug.Log("Building repaired! Happiness boosted by 15.");
    }
}
