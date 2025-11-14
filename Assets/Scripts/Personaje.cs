using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Personaje : MonoBehaviour
{
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Damaged = Animator.StringToHash("Damaged");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private bool jumping = false;
    private bool atacking = false;
    private bool movDerecha = false;
    private bool movIzquierda = false;
    private bool isDead = false;
    private bool isGrounded = true;

    private AudioSource audioSource;
    private Collider2D collider2D;
    private Rigidbody2D rb;
    private float lookHorizontal;

    private int monedas = 0;    

    public int vidas = 2;
    public float fuerzaSalto = 400.0f;
    public float fuerzaSaltoHor = 40.0f;
    public float speed = 100f;

    public Animator anim;
    public GameObject ataqueOriginal;
    public GameObject ataquePos;
    public AudioClip saltoSound;
    public AudioClip atackSound;
    public GameObject limiteIzq;
    
    public float limitIzq = -3.5f;
    public float limitDer = 8.2f;

    public int Vidas{
        get => vidas;
        set => vidas = value;
    }

    public int Monedas{
        get => monedas;
        set => monedas = value;
    }



    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        lookHorizontal = transform.localScale.x;
        
    }

    // Update is called once per frame
    private void Update() {
        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //horizontal = Mathf.Clamp(horizontal, -3.5f, 8.2f);
        if (!jumping)
        {
            transform.Translate(new Vector3(Mathf.Clamp(horizontal, -3.5f, 8.2f), 0.0f));
            anim.SetFloat(Speed, Mathf.Abs(horizontal));
        }
        
        //Limitar personaje en x
        if (transform.position.x < limitIzq)
        {
            transform.position = new Vector3(limitIzq, transform.position.y, transform.position.z);
        }
        if (transform.position.x > limitDer)
        {
            transform.position = new Vector3(limitDer, transform.position.y, transform.position.z);
        }
        
        //Flip personaje
        if (horizontal > 0.01f && !jumping){
            transform.localScale = new Vector3(lookHorizontal , transform.localScale.y, transform.localScale.z);
        } else if (horizontal < -0.01f && !jumping){
            transform.localScale = new Vector3(-lookHorizontal, transform.localScale.y, transform.localScale.z);
        }

        //Saltar
        if (Input.GetAxis("Vertical") > 0.1f && !isDead)
        {
            Saltar();
        }
        
    }

    void OnCollisionEnter2D(Collision2D col){
        //Toca suelo
        if (col.gameObject.CompareTag("suelo")){            
            jumping = false;
            isGrounded = true;
            anim.SetBool(Jumping, jumping);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("suelo")){
            isGrounded = false;
        }
    }

    private void Saltar()
    {
        if (jumping  || rb.linearVelocityY > 0) return;
        rb.AddForce(new Vector2(fuerzaSaltoHor * transform.localScale.x, fuerzaSalto));            
        jumping = true;
        anim.SetBool(Jumping, jumping);

        audioSource.clip = saltoSound;
        audioSource.Play();
    }
}
