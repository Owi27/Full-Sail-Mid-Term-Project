using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody kunai;
    public float playerVelocity = 5.0f;
    public int AmountOfKunais = 3;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            transform.Rotate(Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(-Vector3.forward * Time.deltaTime);        
        }

        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            transform.Rotate(Vector3.right * Time.deltaTime);
        }

        //Kunai Throw with Left Click
        if (Input.GetMouseButtonDown(0))
        {
            if (AmountOfKunais > 0)
            {
                CreateKunai();
            }
            else 
            {
                Debug.Log("Out of Kunais");
            }
        }

        //Reload Kunais
        if (Input.GetKey(KeyCode.R)) 
        {
            Reload();
        }




    }

    void CreateKunai() 
    {
        
        Rigidbody clone = Instantiate(kunai, transform.position, transform.rotation);
        AmountOfKunais -= 1;
        Debug.Log("Kunais left: " + AmountOfKunais); 
    
    }

    void Reload() 
    {
        AmountOfKunais = 3;
        Debug.Log("Refilled Kunais");
    }

    

}

