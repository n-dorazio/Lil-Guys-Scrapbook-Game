using System.Collections.Generic;
using UnityEngine;

public class PlantDatabase : MonoBehaviour
{
    public static PlantDatabase Instance { get; private set; }
    public List<Plant> Plants { get; private set; } = new List<Plant>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDatabase()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("PlantResources/plants"); // Load plants.json from Resources
        if (jsonFile == null)
        {
            Debug.LogError("Failed to load plants database!");
            Plants = new List<Plant>();
            return;
        }

        Plants = JsonUtility.FromJson<PlantList>(jsonFile.text).plants;
        Debug.Log("Plant database loaded successfully.");
    }

    [System.Serializable]
    private class PlantList
    {
        public List<Plant> plants;
    }

    public Plant GetPlantByName(string name)
    {
        return Plants.Find(plant => plant.name == name);
    }
}