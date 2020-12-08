using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MensagemElevador : MonoBehaviour
{
    public Text texto;
    [Range(0.1f, 5f)]
    public float distancia = 3;
    private GameObject Jogador;

    void Start()
    {
        texto.enabled = false;
        Jogador = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,Jogador.transform.position) < distancia)
        {
            texto.enabled = true;
        }
        else
        {
            texto.enabled = false;
        }
    }
}
