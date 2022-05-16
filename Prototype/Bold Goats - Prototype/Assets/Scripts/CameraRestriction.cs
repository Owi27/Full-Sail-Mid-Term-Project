using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CameraRestriction : MonoBehaviour
{
    public float RestrictMinAngle = -55f;
    public float RestrictMaxAngle = 70f;
    private void Update()
    {
        Vector3 Rotation = gameObject.transform.localEulerAngles;

        //if (Rotation.x < RestrictMinAngle)
        //{
        //    gameObject.transform.localEulerAngles = new Vector3(RestrictMinAngle, Rotation.y, Rotation.z);
        //}

        if (Rotation.x > RestrictMaxAngle)
        {
            gameObject.transform.localEulerAngles = new Vector3(RestrictMaxAngle, Rotation.y, Rotation.z);
        }
    }
}
