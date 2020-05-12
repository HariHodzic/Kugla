using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KuglaMovement : MonoBehaviour
{
    [SerializeField]
    [Range(2,50)]
    private float SideSpeed=6000.0f;
    [SerializeField]
    [Range(2, 50)]
    private float ForwardSpeed = 4000.0f;
    [SerializeField]
    private Rigidbody kugla;
    bool LeftSide=true;

    private GameObject Terrain;

    void GoLeft(Rigidbody rb)
    {
        rb.AddForce(ForwardSpeed ,0, SideSpeed,ForceMode.VelocityChange);
    }
    void GoRight(Rigidbody rb)
    { 
        rb.AddForce(ForwardSpeed, 0, -SideSpeed,ForceMode.VelocityChange);
    }
    private void Start()
    {
        Rigidbody kugla = GetComponent<Rigidbody>();
        GoLeft(kugla);
        Terrain=GameObject.FindGameObjectWithTag(Constants.TerrainTag);
    }
    private void OnCollisionEnter(Collision collision)
    {
        string wallTag = Constants.WallTag;
        if (collision.collider.tag == wallTag)
        {
            Rigidbody kugla = GetComponent<Rigidbody>();
            if (LeftSide)
                GoRight(kugla);
            else
                GoLeft(kugla);
            LeftSide = !LeftSide;
        }
    }
    private void FixedUpdate()
    {
        Rigidbody kugla = GetComponent<Rigidbody>();
        //if (LeftSide)
        //    GoLeft(kugla);
        //else
        //    GoRight(kugla);
        if (kugla.transform.position.y < 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        if (kugla.transform.position.x%20<1)
        {
            Terrain.transform.localScale+=new Vector3(20,0,0);
        }
            
    }
}
