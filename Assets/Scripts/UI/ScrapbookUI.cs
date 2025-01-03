using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrapbookUI : MonoBehaviour
{
    public static ScrapbookUI Instance { get; private set; }
    private Plant selectedPlant;

    [SerializeField] private Transform mainPageContent;
    [SerializeField] private GameObject scrapbookEntryPrefab;
    [SerializeField] private ScrapbookDetailPage detailPage;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject scrapbookPanel;
    
    public int CurrentPage { get; private set; } = 0;
    public int ItemsPerPage = 9;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);
        PopulateMainPage();
        detailPage.gameObject.SetActive(false);
    }

    public void PopulateMainPage()
    {
        // Clear existing entries
        foreach (Transform child in mainPageContent)
        {
            Destroy(child.gameObject);
        }

        // Calculate page bounds
        int startIndex = CurrentPage * ItemsPerPage;
        int endIndex = Mathf.Min(startIndex + ItemsPerPage, PlantDatabase.Instance.Plants.Count);

        // Add entries for current page
        for (int i = startIndex; i < endIndex; i++)
        {
            Plant plant = PlantDatabase.Instance.Plants[i];
            GameObject entryObj = Instantiate(scrapbookEntryPrefab, mainPageContent);
            ScrapbookEntry entry = entryObj.GetComponent<ScrapbookEntry>();
            entry.Initialize(plant, plant.isUnlocked);
        }

        UpdateNavigationButtons();
    }

    private void UpdateNavigationButtons()
    {
        previousButton.interactable = CurrentPage > 0;
        nextButton.interactable = (CurrentPage + 1) * ItemsPerPage < PlantDatabase.Instance.Plants.Count;
    }

    public void PreviousPage()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            PopulateMainPage();
        }
    }

    public void NextPage()
    {
        int maxPages = Mathf.CeilToInt(PlantDatabase.Instance.Plants.Count / (float)ItemsPerPage);
        if (CurrentPage < maxPages - 1)
        {
            CurrentPage++;
            PopulateMainPage();
        }
    }

    public void SelectPlant(Plant plant)
    {
        selectedPlant = plant;
        PlantSlot.HighlightAllSlots(true);
    }

    public Plant GetSelectedPlant()
    {
        return selectedPlant;
    }

    public void ClearSelectedPlant()
    {
        selectedPlant = null;
        PlantSlot.HighlightAllSlots(false);
    }

    public void OpenDetailPage(Plant plant)
    {
        detailPage.gameObject.SetActive(true);
        detailPage.Initialize(plant);
    }
} 