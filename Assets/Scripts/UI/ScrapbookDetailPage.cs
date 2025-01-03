using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScrapbookDetailPage : MonoBehaviour
{
    [SerializeField] private Image mainPlantImage;
    [SerializeField] private TextMeshProUGUI plantNameText;
    [SerializeField] private TextMeshProUGUI plantDescriptionText;
    [SerializeField] private Button placeButton;
    [SerializeField] private TextMeshProUGUI placeButtonText;
    [SerializeField] private Transform variantContainer;
    [SerializeField] private Button variantButtonPrefab;
    
    private Plant currentPlant;
    private List<Button> variantButtons = new List<Button>();

    public void Initialize(Plant plant)
    {
        currentPlant = plant;
        UpdateDisplay();
        SetupVariantButtons();
        UpdatePlaceButton();
    }

    private void UpdateDisplay()
    {
        mainPlantImage.sprite = currentPlant.Sprite;
        plantNameText.text = currentPlant.name;
        // Add description when you add it to the Plant class
    }

    private void SetupVariantButtons()
    {
        // Clear existing buttons
        foreach (var button in variantButtons)
        {
            Destroy(button.gameObject);
        }
        variantButtons.Clear();

        // Add variant buttons if they exist
        List<Plant> variants = currentPlant.GetVariants();
        if (variants != null && variants.Count > 0)
        {
            foreach (var variant in variants)
            {
                Button newButton = Instantiate(variantButtonPrefab, variantContainer);
                Image variantImage = newButton.GetComponent<Image>();
                variantImage.sprite = variant.Sprite;
                variantImage.color = variant.isUnlocked ? Color.white : Color.gray;
                
                if (variant.isUnlocked)
                {
                    newButton.onClick.AddListener(() => SwitchToVariant(variant));
                }
                
                variantButtons.Add(newButton);
            }
        }
    }

    private void SwitchToVariant(Plant variant)
    {
        currentPlant = variant;
        UpdateDisplay();
    }

    private void UpdatePlaceButton()
    {
        bool isPlaced = PlantSlot.IsPlantPlaced(currentPlant);
        placeButtonText.text = isPlaced ? "Pick Up" : "Place";
        placeButton.onClick.RemoveAllListeners();
        placeButton.onClick.AddListener(() => OnPlaceButtonClicked(isPlaced));
    }

    private void OnPlaceButtonClicked(bool isPlaced)
    {
        if (isPlaced)
        {
            PlantSlot.RemovePlantFromSlot(currentPlant);
        }
        else
        {
            ScrapbookUI.Instance.SelectPlant(currentPlant);
            gameObject.SetActive(false);
        }
        UpdatePlaceButton();
    }
} 