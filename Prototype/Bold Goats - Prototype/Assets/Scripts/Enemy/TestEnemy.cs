using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class TestEnemy : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject enemyPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shader.SetGlobalFloat("_GlobalVisibility", 1f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Shader.SetGlobalFloat("_GlobalVisibility", .5f);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shader.SetGlobalFloat("_GlobalVisibility", 0f);
        }
    }
}
