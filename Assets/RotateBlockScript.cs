using UnityEngine;

public class RotateBlockScript : MonoBehaviour
{
    public GameObject camera;
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
        Physics2D.gravity = new Vector2(0, 9.81f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        camera.GetComponent<CameraScript>().flip();
        player.transform.rotation = Quaternion.Euler(0, 0, 180);
        player.GetComponent<PlayerScript>().flipped = true;
        player.GetComponent<PlayerScript>().flipControls();
    }
}
