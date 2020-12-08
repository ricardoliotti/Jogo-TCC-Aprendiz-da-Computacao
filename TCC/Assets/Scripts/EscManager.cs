using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscManager : MonoBehaviour
{
    public GameObject escBox;
    public GameObject rankingBox;
    void Start()
    {
        escBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            escBox.SetActive(true);
            FindObjectOfType<controle>().vel = 0f;
        }
        
    }
    public void btn_continuar()
    {
        escBox.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }
    public void btn_menu()
    {
        escBox.SetActive(false);
        SceneManager.LoadScene("Menu");
        FindObjectOfType<controle>().vel = 5f;

    }
    public void btn_sair()
    {
        escBox.SetActive(false);
        Application.Quit();
        FindObjectOfType<controle>().vel = 5f;
    }

    public void btn_pontuacao()
    {
        rankingBox.SetActive(true);
        escBox.SetActive(false);

    }
}
