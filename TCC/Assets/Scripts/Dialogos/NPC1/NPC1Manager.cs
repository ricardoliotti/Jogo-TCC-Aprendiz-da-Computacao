using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Manager : MonoBehaviour
{
    public static NPC1Manager instance;

    public GameObject InfoBox1UI;
    public GameObject InfoBox2UI;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instancia ja existe");
            Destroy(this);
        }
    }
    // Função que inicializa o dialogo quando o colisor do personagem entrar em contato com o do NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<controle>().vel = 0;
            InfoBox1UI.SetActive(true);

        }
    }
    public void btn_prosseguir()//Função do botão prosseguir
    {
        InfoBox1UI.SetActive(false);
        InfoBox2UI.SetActive(true);
    }
    public void btn_fechar()//Função do botão fechar
    {
        InfoBox1UI.SetActive(false);
        InfoBox2UI.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

}
