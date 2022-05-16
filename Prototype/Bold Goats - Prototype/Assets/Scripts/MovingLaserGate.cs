using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaserGate : MonoBehaviour
{
    public Vector3 EndPos;

    IEnumerator Start()
    {
        var StartPos = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, StartPos, EndPos, 3.0f));
            yield return StartCoroutine(MoveObject(transform, EndPos, StartPos, 3.0f));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 StartPos, Vector3 EndPos, float time)
    {
        var interval = 0.0f;
        var speed = 1.0f / time;
        while (interval < 1.0f)
        {
            interval += Time.deltaTime * speed;
            thisTransform.position = Vector3.Lerp(StartPos, EndPos, interval);
            yield return null;
        }
    }
}
