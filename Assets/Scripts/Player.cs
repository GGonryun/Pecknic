using UnityEngine;

public class Player : MonoBehaviour, IDespawnable
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private Vector2 sensitivity = Vector2.zero;
    [SerializeField] private VectorRange verticalRange = VectorRange.zero;

    [SerializeField] private Arm leftArm;
    [SerializeField] private Arm rightArm;


    private Vector3 moveDirection = Vector3.zero;
    private float rotationDirection = 0f;
    private float rotationY = 0F;


    public void Spawn(Vector3 initialPosition)
    {
        transform.position = initialPosition;
        this.gameObject.SetActive(true);
    }
    public void Despawn()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        leftArm.Collided += Destroy;
        rightArm.Collided += Destroy;
    }

    void OnDisable()
    {
        leftArm.Collided -= Destroy;
        rightArm.Collided -= Destroy;
    }

    void Update()
    {
        Movement();
        Punching();
    }

    void Punching()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftArm.Punch();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rightArm.Punch();
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity.x;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity.y;
        rotationY = Mathf.Clamp(rotationY, verticalRange.min, verticalRange.max);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Destroy(object sender, OnCollisionEventArgs e) 
    {
        e.Collider.gameObject.GetComponent<IDespawnable>().Despawn();
    }

}