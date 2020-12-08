using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class elevador : MonoBehaviour
{
    public bool liberaElevador;
    void Start()
    {
        //Variavel que permite que o script só funcione na área de colisão do elevador
        liberaElevador = false;
    }
    void Update()//Controle do Elevador
    {
        if (liberaElevador == true)
        {
            if (Input.GetKey(KeyCode.Keypad1))
            {
                SceneManager.LoadScene("1andar-CPD");
            }
            if (Input.GetKey(KeyCode.Keypad2))
            {
                SceneManager.LoadScene("2andar");
            }
            if (Input.GetKey(KeyCode.Keypad3))
            {
                SceneManager.LoadScene("3andar");
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                SceneManager.LoadScene("4andar");
            }
        }
    }
    //Funções que indicam se o colisor do Personagem está em contato com o colisor do elevador
     void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
            liberaElevador = true;
         }
     }
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
         {
            liberaElevador = false;
        }
    }
}
