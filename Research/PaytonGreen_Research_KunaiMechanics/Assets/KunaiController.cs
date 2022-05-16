using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    public Transform target;
    public Transform kunaiPosition;
    public Rigidbody kunai;

    public float RotationSpeed;
    public float kunaiVelocity = 10.0f;

    void Start() 
    {
        kunaiPosition = GetComponent<Transform>();

    }
   
    void FixedUpdate()
    {
        if (kunai == null)
        {
          return;
        }

        kunai.velocity = kunaiPosition.forward * kunaiVelocity;
        var kunaiRotation = Quaternion.LookRotation(target.position - kunaiPosition.position);
        kunai.MoveRotation(Quaternion.RotateTowards(kunaiPosition.rotation, kunaiRotation, RotationSpeed));
    }
   

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Target") 
        {
            this.gameObject.SetActive(false);
            Debug.Log("It's a hit!");
        }
    }
}
