using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public variables 

    // GameObjects
    public GameObject character;
    public GameObject CameraCenter;

    // Camera
    public Camera CameraFollow;

    // Offset and Sensitivity
    public float yoffset = 1f;
    public float Sensitivity = 3f;

    // Scroll Sensitivity 
    public float ScrollSensitivity = 5f;
    public float ScrollDampening = 6f;

    // Zoom Setting's 
    public float ZoomMin = 3.5f;
    public float ZoomMax = 10f;
    public float ZoomDef = 5f;
    public float ZoomDist;

    // Collision to a object
    public float CollisionSens;

    // Cameras Restrictors so it doesn't go through the map
    private RaycastHit _CameraHit;
    private Vector3 _CameraDistance;

    private void Start()
    {
        _CameraDistance = CameraFollow.transform.localPosition;
        ZoomDist = ZoomDef;
        _CameraDistance.z = ZoomDist;

        // so you don't have to see the mouse move off the screen
        Cursor.visible = true;
        // Created to lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LateUpdate()
    {
        if (character != null)
        {

            CameraCenter.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + yoffset, character.transform.position.z);

            var rotation = Quaternion.Euler(Mathf.Clamp(CameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity / 2, 0, 70), CameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity, CameraCenter.transform.rotation.eulerAngles.z);

            CameraCenter.transform.rotation = rotation;

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                var ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
                ScrollAmount *= ZoomDist * 0.3f;
                ZoomDist += ScrollAmount;
                ZoomDist = Mathf.Clamp(ZoomDist, ZoomMin, ZoomMax);
            }

            if (_CameraDistance.z != -ZoomDist)
            {
                _CameraDistance.z = Mathf.Lerp(_CameraDistance.z, -ZoomDist, Time.deltaTime * ScrollDampening);
            }

            CameraFollow.transform.localPosition = _CameraDistance;

            GameObject Objects = new GameObject();

            Objects.transform.SetParent(CameraFollow.transform.parent);
            Objects.transform.localPosition = new Vector3(CameraFollow.transform.localPosition.x, CameraFollow.transform.localPosition.y, CameraFollow.transform.localPosition.z - CollisionSens);

            if (Physics.Linecast(CameraCenter.transform.position, Objects.transform.position, out _CameraHit))
            {
                CameraFollow.transform.position = _CameraHit.point;

                var LocalPosition = new Vector3(CameraFollow.transform.localPosition.x, CameraFollow.transform.localPosition.y, CameraFollow.transform.localPosition.z + CollisionSens);

                CameraFollow.transform.localPosition = LocalPosition;
            }

            Destroy(Objects);

            if (CameraFollow.transform.localPosition.z > -1f)
            {
                CameraFollow.transform.localPosition = new Vector3(CameraFollow.transform.localPosition.x, CameraFollow.transform.localPosition.y, -1f);
            }
        }
    }

}
