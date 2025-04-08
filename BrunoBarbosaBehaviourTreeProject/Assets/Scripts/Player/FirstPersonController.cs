using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("referneces camera")]
    /*
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
 
    */

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private Transform orientation;

    [SerializeField] private float RotationX;
    [SerializeField] private float RotationY;

    [SerializeField] private float rotationSpeed;

    [Header("referneces movement")]
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;
    private float drag;
    Vector3 moveDirectionl;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    // Update is called once per frame
    void Update()
    {
        // speedControl();
        Debug.DrawLine(orientation.position, orientation.forward * 1000,Color.red);
        getInput();
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        RotationY -= mouseX;
        RotationX += mouseY;

        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(RotationX, RotationY, 0);

    }

    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        moveDirectionl = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirectionl.normalized * speed * 10f, ForceMode.Force);

        rb.drag = drag;
    }

    private void speedControl()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(velocity.magnitude > speed)
        {
            Vector3 limitedVelocity = velocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
