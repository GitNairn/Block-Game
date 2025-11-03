using Unity.VisualScripting;
using UnityEngine;

public class JumpDetectorScript : MonoBehaviour
{

    public GameObject player;
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
        if (collision.gameObject.tag == "Structure")
        {
            player.GetComponent<PlayerScript>().onGround = true;
            player.GetComponent<PlayerScript>().moveForce = player.GetComponent<PlayerScript>().getInitialMoveForce();
        }
    }
}
