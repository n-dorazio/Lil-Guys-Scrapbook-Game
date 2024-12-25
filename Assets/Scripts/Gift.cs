using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
    public Inventory Inventory 
    { 
        get => inventory;
        set => inventory = value;
    }

    private void OnMouseDown()
    {
        Debug.Log("Gift clicked - attempting to open");
        OpenGift();
    }

    public void OpenGift()
    {
        if (inventory == null)
        {
            Debug.LogError("No inventory reference set on Gift!");
            return;
        }

        if (PlantDatabase.Instance == null || PlantDatabase.Instance.Plants.Count == 0)
        {
            Debug.LogError("Cannot open gift - PlantDatabase instance is missing or empty!");
            return;
        }

        // Select a random plant
        int randomIndex = Random.Range(0, PlantDatabase.Instance.Plants.Count);
        Plant randomPlant = PlantDatabase.Instance.Plants[randomIndex];

        // Add plant to the inventory
        inventory.AddPlant(randomPlant);
        Debug.Log($"Added {randomPlant.name} to inventory.");

        // Remove the gift from the scene
        Destroy(gameObject);
    }
}