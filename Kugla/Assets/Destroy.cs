using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==Constants.KuglaTag)
            BasicPool.Instance.AddToPool(gameObject);
    }

}
