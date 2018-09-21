using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Matthew Blaire September 2018*/
public class MovementController : MonoBehaviour {

    public CharacterController player;

	// Use this for initialization
	void Start () {
		if (player == null)
        {
            player = GetComponent<CharacterController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
