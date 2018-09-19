using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {

    public Animator animator;
    private int state = 0;

	// Use this for initialization
	void Start () {

        if (animator == null)
        {
            try
            {
                animator = GetComponent<Animator>(); //if animator not provided, look for one in 
            }
            catch
            {
                Debug.Log("Animator could not be found");
            }
            
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(ControlsController.controls.fireButton))
        {
            animator.Play("ScifiEnergyPistol2SingleFire"); //Pull the trigger back
        }
        if (Input.GetKeyUp(ControlsController.controls.fireButton))
        {
            animator.Play("ReleaseTrigger", 0, 0.658f); //trigger goes back up
        }
	}

    
}
