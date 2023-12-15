using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Boll : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 4;
    public int brickBreaks;
    int liv = 3;
    public float thrust;
    float angle;
    public GameObject lifeGameObject;
    TextMeshProUGUI lifeComponent;
    public GameObject pointGameObject;
    TextMeshProUGUI pointComponent;
    bool startFrame = true;
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        lifeComponent = lifeGameObject.GetComponent<TextMeshProUGUI>();
        pointComponent = pointGameObject.GetComponent<TextMeshProUGUI>();
        lifeComponent.text = "life: " + liv;
        pointComponent.text = "point: " + brickBreaks;
        

    }
    void Update()
    {
        Vector2 posOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseScreenPos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(posOnScreen, mouseScreenPos);   
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (startFrame)
        {
            shootBall();
            startFrame = false;
        }
        if (speed < 15)
        {
            speed += speed * 0.05f * Time.deltaTime;
        }
        rigid.velocity = rigid.velocity.normalized * speed;
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(0, -3, 0);
            liv -= 1;
            if(liv == 0)
            {
                Destroy(gameObject);
            }
            lifeComponent.text = "life: " + liv;
            shootBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject brick = collision.gameObject;
        Block hitBrick = brick.GetComponent<Block>();
        if (hitBrick != null)
        {
            brickBreaks += 1;
            pointComponent.text = "point: " + brickBreaks;
            hitBrick.Break();
        }
    }

    private void shootBall()
    {
        if (startFrame)
        {
            rigid.AddForce(new Vector2(10, 10));
        }
        else
        {
            rigid.AddRelativeForce(Vector2.left * thrust);
        }
    }
}
