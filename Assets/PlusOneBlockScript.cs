using UnityEngine;

public class PlusOneBlockScript : MonoBehaviour
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
        player.GetComponent<PlayerScript>().giveBlock(BlockType.BASIC);
        Destroy(gameObject);
    }
}
