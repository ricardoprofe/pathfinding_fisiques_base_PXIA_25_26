using UnityEngine;

public class Personaje : MonoBehaviour
{
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private bool jumping;
    private bool isDead;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    private float lookHorizontal;
    
    public float fuerzaSalto = 400.0f;
    public float fuerzaSaltoHor = 40.0f;
    public float speed = 100f;

    public Animator anim;
    public AudioClip saltoSound;

    public float limitIzq = -16f;
    public float limitDer = 34f;
   

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        lookHorizontal = transform.localScale.x;
    }

    private void Update()
    {
        var movement = inputActions.Player.Move.ReadValue<Vector2>();
        var horizontal = movement.x * speed * Time.deltaTime;
        Mover(horizontal);
        LimitarPosicion();
        Orientar(horizontal);

        if (movement.y > 0.1f && !isDead)
        {
            Saltar();
        }
    }

    private void Mover(float horizontal)
    {
        if (!jumping)
        {
            transform.Translate(new Vector3(Mathf.Clamp(horizontal, -3.5f, 8.2f), 0.0f));
            anim.SetFloat(Speed, Mathf.Abs(horizontal));
        }
    }

    private void LimitarPosicion()
    {
        if (transform.position.x < limitIzq)
        {
            transform.position = new Vector3(limitIzq, transform.position.y, transform.position.z);
        }

        if (transform.position.x > limitDer)
        {
            transform.position = new Vector3(limitDer, transform.position.y, transform.position.z);
        }
    }

    private void Orientar(float horizontal)
    {
        if (horizontal > 0.01f && !jumping)
        {
            transform.localScale = new Vector3(lookHorizontal, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontal < -0.01f && !jumping)
        {
            transform.localScale = new Vector3(-lookHorizontal, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("suelo"))
        {
            jumping = false;
            anim.SetBool(Jumping, jumping);
        }
    }

    private void Saltar()
    {
        if (jumping || rb.linearVelocityY > 0) return;

        rb.AddForce(new Vector2(fuerzaSaltoHor * transform.localScale.x, fuerzaSalto));
        jumping = true;
        anim.SetBool(Jumping, jumping);

        audioSource.clip = saltoSound;
        audioSource.Play();
    }
}
