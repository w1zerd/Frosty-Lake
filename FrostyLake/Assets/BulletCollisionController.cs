using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Matthew Blaire September 2018*/
public class BulletCollisionController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        GetComponent<BulletController>().SelfDestruct();
    }

}
