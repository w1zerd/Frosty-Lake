using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Matthew Blaire September 2018*/
public class GameController : MonoBehaviour {

    void SetCursorLock (bool wantedState)
    {
        if (wantedState == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        
    }


	// Use this for initialization
	void Start () {
        SetCursorLock(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }
	}
}
