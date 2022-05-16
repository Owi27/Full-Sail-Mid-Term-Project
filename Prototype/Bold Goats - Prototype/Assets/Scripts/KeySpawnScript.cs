using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnScript : MonoBehaviour
{
    [SerializeField] GameObject key;
    public Transform[] keySpawnPoints;
    void Awake()
    {
        int rng = Random.Range(0, keySpawnPoints.Length);
        key.transform.position = keySpawnPoints[rng].position;
    }
}
