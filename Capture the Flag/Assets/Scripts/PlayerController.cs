using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;     //movement speed per second
    public float jumpforce;     //force applied upward 

    public int curHp;       //current health of the player
    public int maxHp;       //max health of the player

    [Header("Mouse Look")]
    public float lookSensitivity;       //mouse sensitivity
    public float maxLookX;              //lowest we can look down
    public float minLookX;              //highest we can look up
    private float rotX;                 //current x rotation

    private Camera camera;      //reference camera
    private Rigidbody rb;       //player rigidbody
    //private Weapon weapon;      //weapon reference

    void Awake()
    {
        curHp = maxHp;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;      // get left and right input
        float z = Input.GetAxis("Vertical") * moveSpeed;        // get forward and back input

        //move relative to camera
        Vector3 dir = transform.right * x + transform.forward * z;
        
        dir.y = rb.velocity.y; 
        rb.velocity = dir;      //apply forve in direction of camera
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") - lookSensitivity;

        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        camera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
