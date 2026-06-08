using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints; // 방마다 찍어둔 포인트들
    public int spawnCount = 10;

    void Start()
    {
        if (spawnPoints.Length == 0) return;

        // spawnPoints 섞기
        Transform[] shuffled = (Transform[])spawnPoints.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int rand = Random.Range(i, shuffled.Length);
            (shuffled[i], shuffled[rand]) = (shuffled[rand], shuffled[i]);
        }

        // 앞에서 spawnCount개만 스폰
        int count = Mathf.Min(spawnCount, shuffled.Length);
        for (int i = 0; i < count; i++)
            Instantiate(keyPrefab, shuffled[i].position, Quaternion.identity);
    }
}