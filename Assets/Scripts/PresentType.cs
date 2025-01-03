using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PresentType
{
    public string name;                     // Name of the present type
    public string spritePath;               // Path for the gift sprite
    public List<LootEntry> lootPool;        // List of loot entries

    private Sprite _giftSprite;             // Cached gift sprite

    // Dynamically load the gift sprite
    public Sprite GiftSprite
    {
        get
        {
            if (_giftSprite == null && !string.IsNullOrEmpty(spritePath))
            {
                _giftSprite = Resources.Load<Sprite>(spritePath);
                if (_giftSprite == null)
                {
                    Debug.LogError($"Gift sprite not found at path: {spritePath}");
                }
            }
            return _giftSprite;
        }
    }
}

[System.Serializable]
public class LootEntry
{
    public string plantName;    // Name of the plant
    public int weight;          // Chance weight for the plant
}