using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Plant> CollectedPlants { get; private set; } = new List<Plant>();

    public void PopulateInventory()
    {
        if (PlantDatabase.Instance == null)
        {
            Debug.LogError("PlantDatabase.Instance is null. Ensure the PlantDatabase is initialized.");
            return;
        }

        foreach (Plant plant in PlantDatabase.Instance.Plants)
        {
            if (plant.isUnlocked)
            {
                AddPlant(plant);
            }
        }
    }


    public void AddPlant(Plant plant)
    {
        if (!CollectedPlants.Contains(plant))
        {
            CollectedPlants.Add(plant);
            Debug.Log($"Added {plant.name} to inventory.");
        }
        else
        {
            Debug.Log($"{plant.name} is already in the inventory.");
        }
    }
}