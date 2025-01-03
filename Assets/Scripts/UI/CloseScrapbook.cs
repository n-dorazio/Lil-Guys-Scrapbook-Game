using UnityEngine;
using UnityEngine.EventSystems;

public class CloseScrapbook : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject scrapbookUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (scrapbookUI != null)
        {
            scrapbookUI.SetActive(false); // Close the inventory
            Debug.Log("Inventory closed by clicking outside.");
        }
    }
}