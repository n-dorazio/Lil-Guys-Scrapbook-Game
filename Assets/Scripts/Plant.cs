using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlantVariant
{
    public string name;
    public string spritePath;
    public bool isUnlocked;
}

[System.Serializable]
public class Plant
{
    public string name;
    public string spritePath;
    public bool canHang;
    public bool isUnlocked;
    public List<PlantVariant> variants;

    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            if (_sprite == null && !string.IsNullOrEmpty(spritePath))
            {
                _sprite = Resources.Load<Sprite>(spritePath);
                if (_sprite == null)
                {
                    Debug.LogError($"Sprite not found at path: {spritePath}");
                }
            }
            return _sprite;
        }
    }

    public List<Plant> GetVariants()
    {
        List<Plant> variantPlants = new List<Plant>();
        if (variants != null)
        {
            foreach (PlantVariant variant in variants)
            {
                Plant variantPlant = new Plant
                {
                    name = variant.name,
                    spritePath = variant.spritePath,
                    canHang = this.canHang, // Inherit hanging property from parent
                    isUnlocked = variant.isUnlocked
                };
                variantPlants.Add(variantPlant);
            }
        }
        return variantPlants;
    }
}