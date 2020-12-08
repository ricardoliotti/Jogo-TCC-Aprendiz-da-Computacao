using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC3Manager : MonoBehaviour
{
    public static NPC3Manager instance;


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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<controle>().vel = 0;
            InfoBox1UI.SetActive(true);

        }
    }

    public void btn_prosseguir()
    {
        InfoBox1UI.SetActive(false);
        InfoBox2UI.SetActive(true);
    }
    public void btn_fechar()
    {
        InfoBox1UI.SetActive(false);
        InfoBox2UI.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

}