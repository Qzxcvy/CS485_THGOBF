using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Transform feet;
    public LayerMask ground;
    private float jumpHeight;
    private Vector3 direction;
    private Rigidbody rBody;
    private float rotationSpeed;
    private float rotationX;
    private float rotationY;
    private new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        speed = 7.0f;
        rotationSpeed = 2f;
        rotationX = 0;
        rotationY = 10f;
        jumpHeight = 5.0f;
        rBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;
        if (direction.x != 0)
        {
            rBody.MovePosition(rBody.position + transform.right * direction.x * speed * Time.deltaTime);
        }
        if (direction.z != 0)
        {
            rBody.MovePosition(rBody.position + transform.forward * direction.z * speed * Time.deltaTime);
        }
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           
            rBody.constraints = ~RigidbodyConstraints.FreezePositionY;
            rBody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }

    }
}
