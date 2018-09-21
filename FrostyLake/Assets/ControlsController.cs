using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Matthew Blaire September 2018*/
public class ControlsController : MonoBehaviour {
    public static ControlsController controls;
    public KeyCode fireButton, jumpButton, forwardButton, strafeLeftButton, strafeRightButton, backwardsButton;
    
	// Use this for initialization
	void Start () {
        controls = GetComponent<ControlsController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
