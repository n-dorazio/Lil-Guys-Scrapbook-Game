using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapbookEntry : MonoBehaviour
{
    [SerializeField] private Image plantImage;
    [SerializeField] private Image stampFrame;
    [SerializeField] private TextMeshProUGUI plantNameText;
    [SerializeField] private Button entryButton;
    [SerializeField] private Sprite defaultLockedSprite;

    private Plant plantData;
    private bool isUnlocked;

    public void Initialize(Plant plant, bool unlocked)
    {
        plantData = plant;
        isUnlocked = unlocked;

        // Set up visuals
        stampFrame.sprite = unlocked ? stampFrame.sprite : defaultLockedSprite;
        plantImage.sprite = plant.Sprite;
        plantImage.gameObject.SetActive(unlocked);
        plantNameText.text = unlocked ? plant.name : "?????";

        // Set up button
        entryButton.onClick.RemoveAllListeners();
        entryButton.onClick.AddListener(OnEntryClicked);
    }

    private void OnEntryClicked()
    {
        if (isUnlocked)
        {
            ScrapbookUI.Instance.OpenDetailPage(plantData);
        }
    }
} 