using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Text txtAcerto;
    public Text txtErro;
    public Text txtTotal;


    private void OnEnable()
    {
        txtAcerto.text = Manager.Instance.countAcerto.ToString();
        txtErro.text = Manager.Instance.countErro.ToString();
        txtTotal.text = Manager.Instance.countTotal.ToString();
    }

    public void btn_fechar()
    {
        this.gameObject.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }
}
