using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboManager : MonoBehaviour
{
    public static RoboManager instance;


    public GameObject InfoBoxUI;
    public GameObject RedesBoxUI;
    public GameObject SoftwareBoxUI;
    public GameObject HardwareBoxUI;

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
            InfoBoxUI.SetActive(true);

        }
    }
    public void RedesScreen()
    {
        InfoBoxUI.SetActive(false);
        RedesBoxUI.SetActive(true);
        SoftwareBoxUI.SetActive(false);
        HardwareBoxUI.SetActive(false);
    }
    public void Softwarecreen()
    {
        InfoBoxUI.SetActive(false);
        RedesBoxUI.SetActive(false);
        SoftwareBoxUI.SetActive(true);
        HardwareBoxUI.SetActive(false);
    }
    public void HardwareScreen()
    {
        InfoBoxUI.SetActive(false);
        RedesBoxUI.SetActive(false);
        SoftwareBoxUI.SetActive(false);
        HardwareBoxUI.SetActive(true);
    }
    public void btn_voltar()
    {
        InfoBoxUI.SetActive(true);
        RedesBoxUI.SetActive(false);
        SoftwareBoxUI.SetActive(false);
        HardwareBoxUI.SetActive(false);
    }
    public void btn_fechar()
    {
        InfoBoxUI.SetActive(false);
        RedesBoxUI.SetActive(false);
        SoftwareBoxUI.SetActive(false);
        HardwareBoxUI.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

}