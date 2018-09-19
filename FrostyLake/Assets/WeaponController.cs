using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private GameObject shoot(GameObject bulletPrefab, Vector3 targetPosition, Vector3 origin)
    {


        

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cam.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(cam.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(cam.transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        Vector3 direction = (targetPosition - origin).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        GameObject instantiatedBullet = Instantiate(bulletPrefab, origin, lookRotation);
        try
        {
            instantiatedBullet.transform.LookAt(hit.point);
            instantiatedBullet.GetComponent<BulletController>().SetBulletSpeed((float)bulletSpeed);
        }
        catch
        {
            Debug.Log("couldnt find bulletcontroller");
        }
        
        return instantiatedBullet;
    }

    private Camera FindCamera() //this function finds and returns the camera. if not found in parent, will look through the scene
    {
        Camera cam;
        try
        {
            return GetComponentInParent<Camera>();
        }
        catch
        {
            Debug.Log("Couldn't find camera, trying again...");
            cam = Transform.FindObjectOfType<Camera>();
        }
        return cam;
    }

    public List<GameObject> activeBullets = new List<GameObject>();

    #region Variables
    private enum BulletType //Enum to pick how gun shoots
    {
        hitscan, // instant, just checks for hit
        projectile // takes time, actual visual projectile
    }

    [Header("Weapon Parameters")]

    [SerializeField]
    [Tooltip("How does this weapon shoot?")]
    private BulletType fireMode;

    [SerializeField]
    private GameObject projectilePrefab; //Prefab to be used as a projectile

    [SerializeField]
    private GameObject impactPrefab; //Prefab to be used when the projectile hits something

    [SerializeField]
    private double bulletSpeed; //Self explainatory

    [SerializeField]
    private Camera cam; // camera used for bullet direction

    [SerializeField]
    private Transform origin; // origin used for projectiles
    #endregion

    void Start () {
        if (cam == null) //Find the camera if not provided
        {
            cam = FindCamera();
        }
        
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(ControlsController.controls.fireButton))
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;

            Ray ray = cam.ScreenPointToRay(new Vector3(x, y, 0));
            Vector3 target = ray.direction;
            activeBullets.Add((GameObject)shoot(projectilePrefab, target, origin.position));
        }
		
	}
}
