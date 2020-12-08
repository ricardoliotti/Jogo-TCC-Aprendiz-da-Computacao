using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controle : MonoBehaviour
{
    public float vel = 5.0f;// Velocidade do movimento

    public Animator anim; // Animação do movimento
    void Update()// Controles do movimento
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-vel * Time.deltaTime, 0));
            transform.localScale = new Vector3(-2, 2, 2);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector2(0, vel * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector2(0, -vel * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", false);
        }
    }
}
