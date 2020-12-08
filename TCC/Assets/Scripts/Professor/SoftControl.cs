using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class SoftControl : MonoBehaviour
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

    public GameObject SoftBox;
    public GameObject InfoBox;


    // Start is called before the first frame update
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://banco-jogotcc.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GravarSoft()
    {
        if (perguntaInput.text.Equals("") && respAInput.text.Equals("") && respBInput.text.Equals("") && respCInput.text.Equals("") && dicaInput.text.Equals("") && InputCorreta.text.Equals(""))
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Existe campos vazios!";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }

        GravarDadosSoft(InputID.text.Trim(), perguntaInput.text.Trim(), respAInput.text.Trim(), respBInput.text.Trim(), respCInput.text.Trim(), dicaInput.text.Trim(), InputCorreta.text.Trim());
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

    void Update()
    {
        if (isMostra)
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

    void GravarDadosSoft(string softID, string pergunta, string A, string B, string C, string dica, string correta)
    {
        Software software = new Software(pergunta, A, B, C, dica, correta);
        string json = JsonUtility.ToJson(software);

        reference.Child("software").Child(softID).SetRawJsonValueAsync(json);
    }

    public void ExcluirSoft()
    {
        if (InputID.text.Equals(""))
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Campo ID vazio";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }

        ExcluirDadosSoft(InputID.text.Trim());

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

    void ExcluirDadosSoft(string softID)
    {
        reference.Child("software").Child(softID).RemoveValueAsync();
    }

    public void consultaSoft()
    {
        MostraDadosSoft(InputID.text.Trim());
    }

    public void btn_voltar()
    {
        InfoBox.SetActive(true);
        SoftBox.SetActive(false);
        feedbackTXT.text = "";
        InputID.text = "";
        InputCorreta.text = "";
        perguntaInput.text = "";
        respAInput.text = "";
        respBInput.text = "";
        respCInput.text = "";
        dicaInput.text = "";
    }

    void MostraDadosSoft(string softID)
    {
        FirebaseDatabase.DefaultInstance.GetReference("software").GetValueAsync().ContinueWith(task =>
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

                string json = snapshot.Child(softID.ToString()).GetRawJsonValue();

                Software extrairDadosSoft = JsonUtility.FromJson<Software>(json);

                recebePergunta = extrairDadosSoft.pergunta;
                recebeRespA = extrairDadosSoft.A;
                recebeRespB = extrairDadosSoft.B;
                recebeRespC = extrairDadosSoft.C;
                recebeCorreta = extrairDadosSoft.correta;
                recebeDica = extrairDadosSoft.dica;

                isMostra = true;
            }
        });
    }
}

public class Software
{
    public string pergunta;
    public string A;
    public string B;
    public string C;
    public string correta;
    public string dica;

    public Software(string pergunta, string A, string B, string C, string dica, string correta)
    {
        this.pergunta = pergunta;
        this.A = A;
        this.B = B;
        this.C = C;
        this.correta = correta;
        this.dica = dica;
    }
}
