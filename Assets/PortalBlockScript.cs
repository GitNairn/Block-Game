using UnityEngine;

public class PortalBlockScript : MonoBehaviour
{

    public GameObject exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && exit != null)
        {
            Vector3 exitPosition = exit.transform.position;
            //exitPosition.y += 1.0f; // Adjust the y position to avoid immediate re-collision
            collision.gameObject.transform.position = exitPosition;
        }
    }
}
