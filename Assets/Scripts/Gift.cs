using UnityEngine;

public class Gift : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Gift clicked - attempting to open");
        OpenGift();
    }

    public void OpenGift()
    {
        if (PlantDatabase.Instance == null || PlantDatabase.Instance.Plants.Count == 0)
        {
            Debug.LogError("Cannot open gift - PlantDatabase instance is missing or empty!");
            return;
        }

        // Select a random plant
        int randomIndex = Random.Range(0, PlantDatabase.Instance.Plants.Count);
        Plant randomPlant = PlantDatabase.Instance.Plants[randomIndex];

        // Unlock the plant
        randomPlant.isUnlocked = true;
        Debug.Log($"Unlocked {randomPlant.name}");

        // Update the UI if it's visible
        //Add animation of plant appearing in book
        ScrapbookUI.Instance?.PopulateMainPage();

        // Remove the gift from the scene
        Destroy(gameObject);
    }
}