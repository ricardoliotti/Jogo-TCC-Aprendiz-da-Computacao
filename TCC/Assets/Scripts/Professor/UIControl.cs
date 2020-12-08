using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject InfoBox;
    public GameObject InstrucaoBox;
    public GameObject Instrucao2Box;
    public GameObject RedeBox;
    public GameObject SoftBox;
    public GameObject HardBox;

    void Start()
    {
        InstrucaoBox.SetActive(false);
        Instrucao2Box.SetActive(false);
        RedeBox.SetActive(false);
        SoftBox.SetActive(false);
        HardBox.SetActive(false);
    }



    public void btn_redes()
    {
        InfoBox.SetActive(false);
        RedeBox.SetActive(true);
    }

    public void btn_soft()
    {
        InfoBox.SetActive(false);
        SoftBox.SetActive(true);
    }

    public void btn_hard()
    {
        InfoBox.SetActive(false);
        HardBox.SetActive(true);
    }

    public void btn_Instrucao()
    {
        InfoBox.SetActive(false);
        InstrucaoBox.SetActive(true);
    }

    public void btn_continuaInstrucao()
    {
        InstrucaoBox.SetActive(false);
        Instrucao2Box.SetActive(true);
    }

    public void btn_voltaInstrucao2()
    {

        Instrucao2Box.SetActive(false);
        InfoBox.SetActive(true);

    }

}
