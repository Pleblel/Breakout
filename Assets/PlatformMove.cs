using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector2(-speed, 0) * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector2(speed, 0) * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
