using UnityEngine;
using System.Collections;

public class PresentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject presentPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minSpawnTime = 300f; // 5 minutes
    [SerializeField] private float maxSpawnTime = 1800f; // 30 minutes

    public GameObject CurrentPresent { get; private set; }

    private void Start()
    {
        Debug.Log("PresentSpawner started");
        if (presentPrefab == null)
        {
            Debug.LogError("Present prefab is not assigned!");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }
        StartCoroutine(SpawnPresents());
    }

    private IEnumerator SpawnPresents()
    {
        while (true)
        {
            if (CurrentPresent == null)
            {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
                Debug.Log($"Waiting {waitTime} seconds until next present spawn");
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Attempting to spawn present");
                SpawnPresent();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnPresent()
    {
        if (presentPrefab == null || spawnPoint == null || CurrentPresent != null)
        {
            Debug.LogError("Missing required references for spawning present or present already exists!");
            return;
        }

        CurrentPresent = Instantiate(presentPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"Present spawned at position: {spawnPoint.position}");
        
        CurrentPresent.transform.SetParent(transform);
    }

    public void ClearCurrentPresent()
    {
        CurrentPresent = null;
    }
} 