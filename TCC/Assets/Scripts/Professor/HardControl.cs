using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class HardControl : MonoBehaviour
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

    public GameObject HardBox;
    public GameObject InfoBox;


    // Start is called before the first frame update
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://banco-jogotcc.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GravarHard()
    {
        if (perguntaInput.text.Equals("") && respAInput.text.Equals("") && respBInput.text.Equals("") && respCInput.text.Equals("") && dicaInput.text.Equals("") && InputCorreta.text.Equals(""))
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Existe campos vazios!";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }

        GravarDadosHard(InputID.text.Trim(), perguntaInput.text.Trim(), respAInput.text.Trim(), respBInput.text.Trim(), respCInput.text.Trim(), dicaInput.text.Trim(), InputCorreta.text.Trim());
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

    void GravarDadosHard(string hardID, string pergunta, string A, string B, string C, string dica, string correta)
    {
        Hardware hardware = new Hardware(pergunta, A, B, C, dica, correta);
        string json = JsonUtility.ToJson(hardware);

        reference.Child("hardware").Child(hardID).SetRawJsonValueAsync(json);
    }

    public void ExcluirHard()
    {
        if (InputID.text.Equals(""))
        {
            feedbackTXT.CrossFadeAlpha(100f, 0f, false);
            feedbackTXT.color = Color.red;
            feedbackTXT.text = "Campo ID vazio";
            feedbackTXT.CrossFadeAlpha(0f, 3f, false);
            return;
        }

        ExcluirDadosHard(InputID.text.Trim());

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

    void ExcluirDadosHard(string hardID)
    {
        reference.Child("hardware").Child(hardID).RemoveValueAsync();
    }

    public void consultaHard()
    {
        MostraDadosHard(InputID.text.Trim());
    }

    public void btn_voltar()
    {
        InfoBox.SetActive(true);
        HardBox.SetActive(false);
        feedbackTXT.text = "";
        InputID.text = "";
        InputCorreta.text = "";
        perguntaInput.text = "";
        respAInput.text = "";
        respBInput.text = "";
        respCInput.text = "";
        dicaInput.text = "";
    }

    void MostraDadosHard(string hardID)
    {
        FirebaseDatabase.DefaultInstance.GetReference("hardware").GetValueAsync().ContinueWith(task =>
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

                string json = snapshot.Child(hardID.ToString()).GetRawJsonValue();

                Hardware extrairDadosHard = JsonUtility.FromJson<Hardware>(json);

                recebePergunta = extrairDadosHard.pergunta;
                recebeRespA = extrairDadosHard.A;
                recebeRespB = extrairDadosHard.B;
                recebeRespC = extrairDadosHard.C;
                recebeCorreta = extrairDadosHard.correta;
                recebeDica = extrairDadosHard.dica;

                isMostra = true;
            }
        });
    }
}

public class Hardware
{
    public string pergunta;
    public string A;
    public string B;
    public string C;
    public string correta;
    public string dica;

    public Hardware(string pergunta, string A, string B, string C, string dica, string correta)
    {
        this.pergunta = pergunta;
        this.A = A;
        this.B = B;
        this.C = C;
        this.correta = correta;
        this.dica = dica;
    }
}
