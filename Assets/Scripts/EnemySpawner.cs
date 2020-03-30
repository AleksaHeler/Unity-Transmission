using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject prefab;
    public int count = 4;
    public float delay = 1f;
    public float interval = 1f;

    void Start() {
        StartCoroutine(Spawn(count, delay, interval));
    }

    IEnumerator Spawn(int count, float delay, float interval)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < count; i++) {
            Instantiate(prefab, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(interval);
        }
    }
}
