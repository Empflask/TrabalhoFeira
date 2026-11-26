using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movercamera : MonoBehaviour
{
    
    void Update()
    {
        


        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(-1, 2, -6);
            transform.rotation = Quaternion.Euler(20, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(-1, 2, 6);
            transform.rotation = Quaternion.Euler(20, 180, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(-7, 2, 0);
            transform.rotation = Quaternion.Euler(20, 90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(4.7f, 2, 0);
            transform.rotation = Quaternion.Euler(20, -90, 0);
        }
    }
}
