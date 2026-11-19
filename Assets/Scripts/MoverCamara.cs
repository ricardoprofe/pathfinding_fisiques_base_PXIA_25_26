using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    public float speed = 0.1f;
    public Transform player;
    
    public float limitIzq = -4f;
    public float limitDer = 22f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Follow player
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, limitIzq, limitDer), targetPosition.y, targetPosition.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }
}
