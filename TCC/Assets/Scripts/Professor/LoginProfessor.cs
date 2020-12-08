using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginProfessor : MonoBehaviour
{
    //Valores de login professor
    private const string Email = "professor@prof.com";
    private const string Senha = "123456";


    [SerializeField]
    private TMP_InputField emailField = null;
    [SerializeField]
    private TMP_InputField senhaField = null;
    [SerializeField]
    private TMP_Text feedbackmsg = null;

    public void FazerLogin() // Funcionalidade do botão Login Professor
    {
        string usuario = emailField.text;
        string senha = senhaField.text;

        if (usuario == Email && senha == Senha)// Validações de Login
        {
            feedbackmsg.CrossFadeAlpha(100f, 0f, false);
            feedbackmsg.color = Color.green;
            feedbackmsg.text = "Login de professor realizado...";
            feedbackmsg.CrossFadeAlpha(0f, 2f, false);
            StartCoroutine(CarregaCena());
        }
        else
        {
            feedbackmsg.CrossFadeAlpha(100f, 0f, false);
            feedbackmsg.color = Color.red;
            feedbackmsg.text = "Credenciais de professor inválidas";
            feedbackmsg.CrossFadeAlpha(0f, 2f, false);
            emailField.text = "";
            senhaField.text = "";
        }
    }
    IEnumerator CarregaCena() // Funcionalidade que carrega a tela do Professor
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Professor");
    }
}

