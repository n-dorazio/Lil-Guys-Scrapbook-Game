using UnityEngine;

[System.Serializable]
public class Plant
{
    public string name;
    public string spritePath;
    public bool canHang;
    public bool isUnlocked;

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
}