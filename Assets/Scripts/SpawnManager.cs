using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject fallingBlockPrefab;
    public Vector2 secondsBetweenSpawns;
    private float nextSpawnTime;
    private Vector2 screenHalfSize;

    public Vector2 minMaxSize;
    public float maxAngle;
    // Start is called before the first frame update
    void Start()
    {
        screenHalfSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        nextSpawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawnTime <= 0) {
            var spawnSize = Random.Range(minMaxSize.x, minMaxSize.y);
            var spawnAngle = Random.Range(-maxAngle, maxAngle);

            nextSpawnTime = Mathf.Lerp(secondsBetweenSpawns.x, secondsBetweenSpawns.y, Difficulty.GetDifficultyPercent());
            var spawnPos = new Vector2(Random.Range(-screenHalfSize.x + spawnSize * 0.5f,screenHalfSize.x - spawnSize * 0.5f), screenHalfSize.y + spawnSize * 0.75f);
            var newBlock = Instantiate(fallingBlockPrefab, spawnPos, Quaternion.Euler(Vector3.forward * spawnAngle));

            newBlock.transform.localScale = Vector2.one * spawnSize;
        }
        else {
            nextSpawnTime -= Time.deltaTime;
        }
    }
}
