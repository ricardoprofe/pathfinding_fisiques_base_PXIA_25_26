using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    public float speed = 0.1f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Follow player
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, -3.5f, 6f), targetPosition.y, targetPosition.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }
}
