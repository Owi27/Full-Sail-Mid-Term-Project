using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RayCaster : MonoBehaviour, IRandomizable
{
    [SerializeField] float rayCastLength;

    public Vector3 directionOne;
    public Vector3 directionTwo;

    [SerializeField] Material hitMaterial;
    [SerializeField] Material initMaterial;

    [SerializeField] GameObject[] collidableObj;

    [SerializeField] LayerMask layerToCheck;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Randomize();
        }


        for (int i = 0; i < collidableObj.Length; i++)
        {
            collidableObj[i].GetComponent<MeshRenderer>().material = initMaterial;
        }

        for (int i = 0; i < 2; i++)
        {
            directionOne = collidableObj[0].GetComponent<Mover>().GetDirectionNormalized(transform.position);
            directionTwo = collidableObj[1].GetComponent<Mover>().GetDirectionNormalized(transform.position);

            dir = i == 0 ? directionOne : directionTwo;
            

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, rayCastLength, layerToCheck, QueryTriggerInteraction.Collide))
            {
                //if ray hit, change the color and debug a message
                hit.transform.GetComponent<MeshRenderer>().material = hitMaterial;
                //Debug.Log("YOU HAVE HIT " + hit.transform.gameObject.name);
            }

        }
    }

    private void OnDrawGizmos()
    {
        ReInitialize();

        for (int i = 0; i < 2; i++)
        {
            Vector3 dir = Vector3.zero;
            dir = i == 0 ? directionOne : directionTwo;
            dir.Normalize();
            Gizmos.DrawRay(transform.position, dir * rayCastLength);
        }

    }

    public void ReInitialize()
    {
        for (int i = 0; i < collidableObj.Length; i++)
        {
            Vector3 dir = collidableObj[i].GetComponent<Mover>().GetDirectionNormalized(transform.position);
            collidableObj[i].transform.position = dir * 7;
            collidableObj[i].GetComponent<Mover>().SetDirection(dir);
        }
    }

    public void Randomize()
    {
        System.Random random = new System.Random((int)Time.time);
        for (int i = 0; i < collidableObj.Length; i++)
        {
            collidableObj[i].transform.position = new Vector3(random.Next(-101, 101), random.Next(-101, 101), 0);
            
        }

        ReInitialize();
    }
}

