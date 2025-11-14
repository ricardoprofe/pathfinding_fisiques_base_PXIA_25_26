using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarPlataformas : MonoBehaviour
{
    public GameObject plataformaOriginal;
    public float probabPlataforma = 5.0f;
    public float[]  plataformasPos = new float[] {1.0f, -1.0f}; 
    private int numPlataformas = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DecideSiPlataforma();
    }

    private void DecideSiPlataforma(){
        float random = Random.Range(0.0f, 100.0f);
        Vector3 vect3 = transform.position;

        int index = Random.Range(0, 2);
        vect3.y = plataformasPos[index];
        if(random < probabPlataforma){
            GameObject.Instantiate(plataformaOriginal, vect3, transform.rotation);
        }
    }
}
