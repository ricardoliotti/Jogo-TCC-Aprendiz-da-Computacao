using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public string nomeDaCena;

    public void mudarCena()
    {
        StartCoroutine(CarregaCena());
    }

    IEnumerator CarregaCena()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nomeDaCena);//Informa o nome da cena que deseja carregar na Unity
    }

    public void Sair()
    {
        Application.Quit();
    }
}
