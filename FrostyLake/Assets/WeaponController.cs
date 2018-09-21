using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private GameObject shoot(GameObject bulletPrefab, Vector3 targetPosition, Vector3 originPos)
    {

        #region ray casting
        RaycastHit hit;
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
        #endregion

        Vector3 direction = (targetPosition - originPos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        GameObject instantiatedBullet = Instantiate(bulletPrefab, originPos, lookRotation);
        try
        {
            instantiatedBullet.transform.LookAt(hit.point);
            instantiatedBullet.GetComponent<BulletController>().SetupBullet(impactPrefab, (float)bulletSpeed, origin, curvePoint, controlPoint);
        }
        catch
        {
            Debug.Log("couldnt find bulletcontroller");
        }

        GameObject muzzleFlash = Instantiate(muzzleFlarePrefab, flashPoint.position, flashPoint.rotation, flashPoint);
        Destroy(muzzleFlash, 1f);

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
    private GameObject projectilePrefab, impactPrefab, muzzleFlarePrefab; //Prefab to be used as a projectile, 
                                                                          //Prefab to be used when the projectile hits something,
                                                                          //Prefab to be used as muzzle flare.


    [SerializeField]
    private double bulletSpeed; //Self explainatory

    [SerializeField]
    private Camera cam; // camera used for bullet direction

    [SerializeField]
    private Transform flashPoint, lazerParent, origin, curvePoint, controlPoint;// origin used for projectiles






    #endregion

    void Start () {
        if (cam == null) //Find the camera if not provided
        {
            cam = FindCamera();
        }
        if ((origin != null) && (curvePoint != null))
        {
            origin.LookAt(curvePoint.position);
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
