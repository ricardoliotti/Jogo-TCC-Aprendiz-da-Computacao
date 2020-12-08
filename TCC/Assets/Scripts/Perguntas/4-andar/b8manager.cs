using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using TMPro;
using UnityEngine.UI;


public class b8manager : MonoBehaviour
{
    DatabaseReference reference;

    public Text mostraPergunta;
    public Text mostraRespostaA;
    public Text mostraRespostaB;
    public Text mostraRespostaC;
    public Text mostraDica;

    public GameObject AcertoBox;
    public GameObject ErroBox;
    public GameObject CanvasPergunta;
    public GameObject InfoBox;
    public GameObject QuestaoBox;
    public GameObject ResolveuBox;

    private bool jaRespondeu = false;


    public string[] respCorreta;


    // Start is called before the first frame update
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://banco-jogotcc.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        respCorreta = new string[6];
    }

    public void loadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("hardware").ValueChanged += b1manager_ValueChanged;
    }

    private void b1manager_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        respCorreta[0] = e.Snapshot.Child("2").Child("correta").GetValue(true).ToString();
        respCorreta[1] = e.Snapshot.Child("2").Child("pergunta").GetValue(true).ToString();
        respCorreta[2] = e.Snapshot.Child("2").Child("A").GetValue(true).ToString();
        respCorreta[3] = e.Snapshot.Child("2").Child("B").GetValue(true).ToString();
        respCorreta[4] = e.Snapshot.Child("2").Child("C").GetValue(true).ToString();
        respCorreta[5] = e.Snapshot.Child("2").Child("dica").GetValue(true).ToString();


        mostraPergunta.text = respCorreta[1];
        mostraRespostaA.text = respCorreta[2];
        mostraRespostaB.text = respCorreta[3];
        mostraRespostaC.text = respCorreta[4];
        mostraDica.text = respCorreta[5];



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (jaRespondeu == true)
            {
                other.GetComponent<controle>().vel = 0;
                ResolveuBox.SetActive(true);
            }
            else if (jaRespondeu == false)
            {
                other.GetComponent<controle>().vel = 0;
                CanvasPergunta.SetActive(true);
                InfoBox.SetActive(true);
            }

        }
    }
    public void InfoScreen() //Botão resolver
    {
        InfoBox.SetActive(false);
        QuestaoBox.SetActive(true);
    }
    public void QuestaoScreen() //Botão fechar
    {
        QuestaoBox.SetActive(false);
        CanvasPergunta.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

    public void ResolveuScreen() // Botão fechar 
    {
        ResolveuBox.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

    public void btn_respostas(Button btn)
    {
        if (btn.GetComponentInChildren<Text>().text == respCorreta[0])
        {
            AcertoBox.SetActive(true);
            Manager.Instance.countAcerto++;
            Manager.Instance.countTotal++;
            jaRespondeu = true;
        }
        else
        {
            ErroBox.SetActive(true);
            Manager.Instance.countErro++;
        }

    }

    public void btn_fecharAcerto()// botao fechar do acerto box
    {
        AcertoBox.SetActive(false);
        CanvasPergunta.SetActive(false);
        InfoBox.SetActive(true);
        QuestaoBox.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }

    public void btn_fecharErro()
    {
        ErroBox.SetActive(false);
    }

}