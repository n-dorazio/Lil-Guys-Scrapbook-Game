using UnityEngine;

public class PlantSlot : MonoBehaviour
{
    [SerializeField] private bool isHangingSlot; // Determines if the slot supports hanging plants
    private PlantBehaviour currentPlant; // The plant currently in this slot
    private SpriteRenderer slotRenderer; // Visual feedback for highlighting the slot

    private static Color highlightColor = new Color(0.5f, 1f, 0.5f, 0.5f); // Greenish transparent color
    private static Color defaultColor = new Color(1f, 1f, 1f, 0f); // Fully transparent color
    private static PlantSlot previousSlot; // Tracks the last slot where the plant was placed

    private void Awake()
    {
        // Add or retrieve a SpriteRenderer for visual representation
        slotRenderer = GetComponent<SpriteRenderer>();
        if (slotRenderer == null)
        {
            slotRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // Set the default color (transparent)
        slotRenderer.color = defaultColor;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Clicked on slot: {name}");

        // Get the currently selected plant from the inventory
        Plant selectedPlant = ScrapbookUI.Instance?.GetSelectedPlant();

        if (selectedPlant == null)
        {
            Debug.Log("No plant selected to place.");
            return;
        }

        // Check if the plant can be placed in this slot
        if (isHangingSlot && !selectedPlant.canHang)
        {
            Debug.Log($"This plant ({selectedPlant.name}) cannot be placed in a hanging slot.");
            return;
        }

        // Place the plant in this slot
        PlacePlant(selectedPlant);

        // Remove the plant from the previous slot, if it's different from the current one
        if (previousSlot != null && previousSlot != this)
        {
            previousSlot.RemovePlant();
        }

        // Update the previous slot to the current one
        previousSlot = this;

        // Clear the plant selection from the inventory
        ScrapbookUI.Instance?.ClearSelectedPlant();
    }

    private void PlacePlant(Plant plant)
    {
        Debug.Log($"Placing {plant.name} in slot: {name}");

        // Remove any existing plant in this slot
        RemovePlant();

        // Spawn a new GameObject for the plant
        GameObject plantObj = new GameObject(plant.name);
        plantObj.transform.SetParent(transform);
        plantObj.transform.localPosition = Vector3.zero;
        plantObj.transform.localScale = Vector3.one * 1.5f; // Scale up the plant to a reasonable size

        // Add a SpriteRenderer for the plant's sprite
        SpriteRenderer spriteRenderer = plantObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = plant.Sprite;

        // Attach a PlantBehaviour to manage the plant's data
        PlantBehaviour behaviour = plantObj.AddComponent<PlantBehaviour>();
        behaviour.SetPlantData(plant);

        // Assign the new plant to the slot
        currentPlant = behaviour;

        Debug.Log($"Plant {plant.name} successfully placed in slot: {name}");
    }

    public void RemovePlant()
    {
        if (currentPlant != null)
        {
            Debug.Log($"Removing plant {currentPlant.PlantData.name} from slot: {name}");
            Destroy(currentPlant.gameObject);
            currentPlant = null;
        }
    }

    public void HighlightSlot(bool highlight)
    {
        // Change the slot's color based on whether it should be highlighted
        slotRenderer.color = highlight ? highlightColor : defaultColor;
    }

    public static void HighlightAllSlots(bool highlight)
    {
        // Highlight or unhighlight all slots
        foreach (PlantSlot slot in FindObjectsOfType<PlantSlot>())
        {
            slot.HighlightSlot(highlight);
        }
    }

    public static bool IsPlantPlaced(Plant plant)
    {
        foreach (PlantSlot slot in FindObjectsOfType<PlantSlot>())
        {
            if (slot.currentPlant != null && slot.currentPlant.PlantData == plant)
            {
                return true;
            }
        }
        return false;
    }

    public static void RemovePlantFromSlot(Plant plant)
    {
        foreach (PlantSlot slot in FindObjectsOfType<PlantSlot>())
        {
            if (slot.currentPlant != null && slot.currentPlant.PlantData == plant)
            {
                slot.RemovePlant();
                return;
            }
        }
    }
}