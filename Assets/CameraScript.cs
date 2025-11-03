using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Boolean flipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        if (flipped)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public void flip()
    {
        offset = new Vector3(offset.x, -offset.y, offset.z);
        transform.rotation = Quaternion.Euler(0, 0, 180) * transform.rotation;
        flipped = true;
    }
}
