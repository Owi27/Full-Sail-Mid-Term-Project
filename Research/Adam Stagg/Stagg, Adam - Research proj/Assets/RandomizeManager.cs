using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeManager : MonoBehaviour, IRandomizable
{
    public void Randomize()
    {
        GameObject.Find("Observer").GetComponent<IRandomizable>().Randomize();
    }
}
