using UnityEngine;

public class ScrapbookButton : MonoBehaviour
{
    [SerializeField] private GameObject scrapbookUI;

    private void OnMouseDown()
    {
        if (scrapbookUI == null)
        {
            Debug.LogError("Inventory UI is not assigned in the Inspector.");
            return;
        }
        bool isCurrentlyActive = scrapbookUI.activeSelf;
        if (isCurrentlyActive == false)
        {
            scrapbookUI.SetActive(true);
        }

        Debug.Log($"Inventory state changed. Active: {scrapbookUI.activeSelf}");
    }
}