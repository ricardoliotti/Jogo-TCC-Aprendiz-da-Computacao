using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class redeControl : MonoBehaviour
{
    DatabaseReference reference;

    public InputField InputID;
    public InputField InputCorreta;
    public InputField perguntaInput;
    public InputField respAInput;
    public InputField respBInput;
    public InputField respCInput;
    public InputField dicaInput;
    public Text feedbackTXT;

    public string recebePergunta = "";
    public string recebeRespA = "";
    public string recebeRespB = "";
    public string recebeRespC = "";
    public string recebeCorreta = "";
    public string recebeDica = "";

    private bool isMostra = false;

    public GameObject redeBox;
    public GameObject InfoBox;


    // Start is called before the first frame update
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://banco-jogotcc.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GravarRede()//Função que grava informações no banco
    {
        if (perguntaInput.text.Equals("") && respAInput.text.Equals("") && respBInput.text.Equals("") 
            && respCInput.text.Equals("") && dicaInput.text.Equals("") && InputCorreta.text.Equals("")) 
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Existe campos vazios!";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }

       GravarDadosRede(InputID.text.Trim(), perguntaInput.text.Trim(), respAInput.text.Trim(), 
                       respBInput.text.Trim(), respCInput.text.Trim(), dicaInput.text.Trim(), 
                       InputCorreta.text.Trim());

        feedbackTXT.CrossFadeAlpha(100f, 0f, false);
        feedbackTXT.color = Color.green;
        feedbackTXT.text = "Pergunta cadastrada!";
        feedbackTXT.CrossFadeAlpha(0f, 3f, false);
        InputID.text = "";
        InputCorreta.text = "";
        perguntaInput.text = "";
        respAInput.text = "";
        respBInput.text = "";
        respCInput.text = "";
        dicaInput.text = "";
    }
    public void btn_voltar()//Função do botão voltar
    {
        InfoBox.SetActive(true);
        redeBox.SetActive(false);
        feedbackTXT.text = "";
        InputID.text = "";
        InputCorreta.text = "";
        perguntaInput.text = "";
        respAInput.text = "";
        respBInput.text = "";
        respCInput.text = "";
        dicaInput.text = "";
    }
    void Update()
    {
        if(isMostra)
        {
            recebeDados();
            isMostra = false;
        }
    }

    void recebeDados()
    {
        perguntaInput.text = recebePergunta;
        respAInput.text = recebeRespA;
        respBInput.text = recebeRespB;
        respCInput.text = recebeRespC;
        InputCorreta.text = recebeCorreta;
        dicaInput.text = recebeDica;
    }
    //Função que cria as chaves no banco
    void GravarDadosRede(string redeID, string pergunta, string A, string B, string C,
                         string dica, string correta)
    {
        Redes redes = new Redes(pergunta, A, B, C, dica, correta);
        string json = JsonUtility.ToJson(redes);

        reference.Child("redes").Child(redeID).SetRawJsonValueAsync(json);
    }

    public void ExcluirRedes()//Função que exclui informações do banco
    {
        if (InputID.text.Equals(""))
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Campo ID vazio";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }
        
        ExcluirDadosRedes(InputID.text.Trim());

        feedbackTXT.CrossFadeAlpha(100f, 0f, false);
        feedbackTXT.color = Color.green;
        feedbackTXT.text = "Pergunta excluída!";
        feedbackTXT.CrossFadeAlpha(0f, 3f, false);
        InputID.text = "";
        perguntaInput.text = "";
        respAInput.text = "";
        respBInput.text = "";
        respCInput.text = "";
        dicaInput.text = "";
    }

    void ExcluirDadosRedes(string redeID)
    {
        reference.Child("redes").Child(redeID).RemoveValueAsync();
    }

    public void consultaRedes()
    {
        MostraDadosRedes(InputID.text.Trim());
    }

    void MostraDadosRedes(string redeID)//Função que consulta informações do banco
    {
        FirebaseDatabase.DefaultInstance.GetReference("redes").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                feedbackTXT.CrossFadeAlpha(100f, 0f, false);
                feedbackTXT.color = Color.red;
                feedbackTXT.text = "Campo ID inexistente";
                feedbackTXT.CrossFadeAlpha(0f, 3f, false);
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                string json = snapshot.Child(redeID.ToString()).GetRawJsonValue();

                Redes extrairDadosRedes = JsonUtility.FromJson<Redes>(json);

                recebePergunta = extrairDadosRedes.pergunta;
                recebeRespA = extrairDadosRedes.A;
                recebeRespB = extrairDadosRedes.B;
                recebeRespC = extrairDadosRedes.C;
                recebeCorreta = extrairDadosRedes.correta;
                recebeDica = extrairDadosRedes.dica;

                isMostra = true;
            }
        });
    }
}

public class Redes
{
    public string pergunta;
    public string A;
    public string B;
    public string C;
    public string correta;
    public string dica;

    public Redes(string pergunta, string A, string B, string C, string dica, string correta)
    {
        this.pergunta = pergunta;
        this.A = A;
        this.B = B;
        this.C = C;
        this.correta = correta;
        this.dica = dica;
    }
}
