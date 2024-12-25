using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    private Plant plantData;

    public Plant PlantData => plantData;

    public void SetPlantData(Plant plant)
    {
        plantData = plant;

        // Assign the sprite to this GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (plant.Sprite != null)
        {
            spriteRenderer.sprite = plant.Sprite; // Use the Sprite property
        }
        else
        {
            Debug.LogError($"Sprite for plant {plant.name} is null!");
        }
    }
}