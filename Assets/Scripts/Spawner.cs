using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float initialSpawnRate = 1f;
    public float minSpawnRate = 0.5f;
    public float spawnRateDecrease = 0.01f;
    public float minHeight = -0.6f;
    public float maxHeight = 0.6f;
    public float verticalGap = 3f;

    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnLoop()
    {
        float currentSpawnRate = initialSpawnRate;

        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(currentSpawnRate);

            if (currentSpawnRate > minSpawnRate)
            {
                currentSpawnRate -= spawnRateDecrease;
            }
        }
    }

    private void Spawn()
    {
        GameObject candels = Instantiate(prefab, transform.position, Quaternion.identity);
        candels.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

        Candels candelScript = candels.GetComponent<Candels>();
        if (candelScript != null)
        {
            candelScript.gap = Random.Range(2.0f, verticalGap);
        }
    }
}