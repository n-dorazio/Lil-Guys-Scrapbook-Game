using System.Collections.Generic;
using UnityEngine;

public class PresentTypeDatabase : MonoBehaviour
{
    public static PresentTypeDatabase Instance { get; private set; }
    public List<PresentType> PresentTypes { get; private set; } = new List<PresentType>();

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
        TextAsset jsonFile = Resources.Load<TextAsset>("PlantResources/presentTypes");
        if (jsonFile == null)
        {
            Debug.LogError("Failed to load presentTypes.json file!");
            return;
        }

        PresentTypes = JsonUtility.FromJson<PresentTypeList>(jsonFile.text).presentTypes;
        Debug.Log("PresentType database loaded successfully.");
    }

    [System.Serializable]
    private class PresentTypeList
    {
        public List<PresentType> presentTypes; // Matches the JSON structure
    }
}