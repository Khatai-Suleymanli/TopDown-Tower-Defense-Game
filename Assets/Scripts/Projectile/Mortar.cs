using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mortar : MonoBehaviour
{
    public GameObject launchSite;
    public GameObject landingTarget;
    public GameObject projectilePrefab; // mortar shell prefab
    public float yMax;
    private float g; // gravity
    private float y_0;
    private float x;
    private float z;


    // boolean
    [Header("booleans")]
    private bool canShootRocket = false;
    public bool canClickButton = true;



    [Header("button objects")]

    public GameObject RealButton;
    public GameObject FakeButton;

    //public Button shootButton;


    private float shootLoadTime = 5f;
    private float lastShootTime;

    void Start()
    {
        // Set parameters that will determine time of flight and velocities required for launch
        g = 9.8f;
        lastShootTime = -shootLoadTime;

        //FakeButton.gameObject.SetActive(false);

}

    public void SetBoolTrue() {
        if(canClickButton){
            canShootRocket = true;
            Debug.Log("Bool is set to true");
            FakeButton.SetActive(true);
            RealButton.SetActive(false);
        }
        
    }

    void Update()
    {
        // If left-mouse button is clicked, launch projectile
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= shootLoadTime && canShootRocket)/*bool true*/
        {

            SetLandingTarget(); // define below
            Debug.Log("Mouse Clicked");
            SpawnAndLaunchShell();

            lastShootTime = Time.time;
            
        }
        
    }


    void SpawnAndLaunchShell() { 
        GameObject new_Projectile = Instantiate(projectilePrefab, launchSite.transform.position, Quaternion.identity);
        LaunchBall(new_Projectile);


    }

    void SetLandingTarget() { 
        // caasting ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) { 
            landingTarget.transform.position = hit.point;

            y_0 = projectilePrefab.transform.position.y - landingTarget.transform.position.y;
            x = landingTarget.transform.position.x - launchSite.transform.position.x;
            z = landingTarget.transform.position.z - launchSite.transform.position.z;

        }
        canClickButton = false;
        canShootRocket = false;

        

        StartCoroutine(startTime());



    }

    IEnumerator startTime() { 
        yield return new WaitForSeconds(5f);
        canClickButton = true;
        FakeButton.SetActive(false);
        RealButton.SetActive(true);
       

    }

    void LaunchBall(GameObject launchObject)
    {
        // Launch projectile using rigid body
        Rigidbody projectileBody = launchObject.GetComponent<Rigidbody>();
        projectileBody.velocity = CalculateVelocity();
    }

    Vector3 CalculateVelocity()
    {
        // Distance X and Z to target location
        Vector3 displacementXZ = new Vector3(x, 0, z);

        // Implement equations derived from kinematic analysis
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * g * (yMax - y_0));
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(2 * (yMax - y_0) / g) + Mathf.Sqrt(2 * yMax / g));

        Vector3 velocity = velocityXZ + velocityY;
        return velocity;
    }

}
