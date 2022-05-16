using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    private Vector3 CameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = .5f;

    public bool LookAtPlayer = false; 
    // Start is called before the first frame update
    void Start()
    {
        CameraOffset = PlayerTransform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 NewPos = PlayerTransform.position + CameraOffset;
        PlayerTransform.position = Vector3.Slerp(PlayerTransform.position, NewPos, SmoothFactor);
        

        if (LookAtPlayer) 
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
