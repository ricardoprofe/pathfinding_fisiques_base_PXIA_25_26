using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Esqueleto : MonoBehaviour
{
    public float speed = 0.2f;

    [SerializeField, Min(0f)] private float gravityScale = 1f;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
