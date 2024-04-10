using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ControlType controllerType;
    public Joystick joystick;
    public float speed;

    public enum ControlType { PC, Android }

    private Rigidbody2D rigidbody;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    private bool facingRight = true;
    public static PlayerMovement Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        transform.position = Vector3.zero;

        //transform.position = Vector3.zero;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (controllerType == ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (controllerType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else if (controllerType == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x < 0)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}