using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform plantGridContainer;
    [SerializeField] private GameObject plantButtonPrefab;

    private Inventory inventory;
    private Plant selectedPlant = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryPanel.SetActive(false); // Start with inventory closed
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        // Clear existing plant buttons
        foreach (Transform child in plantGridContainer)
        {
            Destroy(child.gameObject);
        }

        // Add buttons for each collected plant
        foreach (Plant plant in inventory.CollectedPlants)
        {
            GameObject buttonObj = Instantiate(plantButtonPrefab, plantGridContainer);
            Button button = buttonObj.GetComponent<Button>();
            Image image = buttonObj.GetComponent<Image>();

            if (image != null)
            {
                image.sprite = plant.Sprite; // Display plant sprite
            }

            button.onClick.AddListener(() => SelectPlant(plant));
        }
    }

    private void SelectPlant(Plant plant)
    {
        selectedPlant = plant;
        Debug.Log($"Selected plant: {plant.name}. Now click on a slot to place it.");

        // Close the inventory
        inventoryPanel.SetActive(false);

        // Highlight slots
        PlantSlot.HighlightAllSlots(true);
    }

    public Plant GetSelectedPlant()
    {
        return selectedPlant;
    }

    public void ClearSelectedPlant()
    {
        selectedPlant = null;

        // Stop highlighting slots
        PlantSlot.HighlightAllSlots(false);
    }
}