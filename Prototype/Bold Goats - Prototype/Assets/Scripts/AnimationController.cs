using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    public float SmoothDamp;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
 
    //Update is called once per frame
    void Update()
    {
        //Input 
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        //Movement
        Vector3 Movement = new Vector3(Horizontal, 0f, Vertical);

        //Animation

        Vector3 x = transform.InverseTransformPoint(transform.forward);
        x.Normalize();
        Vector3 z = transform.InverseTransformPoint(transform.right);
        //z.Normalize();

        float VelocityZ = Vector3.Dot(Movement.normalized, x);
        float VelocityX = Vector3.Dot(Movement.normalized, z);

        animator.SetFloat("VelocityZ", VelocityX, SmoothDamp, Time.deltaTime);
        animator.SetFloat("VelocityX", -VelocityZ, SmoothDamp, Time.deltaTime);

    }
}
