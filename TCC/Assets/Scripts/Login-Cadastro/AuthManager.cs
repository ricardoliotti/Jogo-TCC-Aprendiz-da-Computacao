using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    //Firebase variaveis
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;

    //Login variaveis
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variaveis
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public TMP_Text confirmRegisterText;

    void Awake()
    {
        //Checa se as dependencias necessarias do Firebase estão presentes no sistema
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Não pode resolver todas dependencias do Firebase: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Iniciando Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
    }

    //Funcionalidade do botão Login(Play)
    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Funcionalidade do botão Register(Cadastrar)
    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    private IEnumerator Login(string _email, string _password) //Validações de Login
    { 
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falha para registrar {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Falhou!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Falta o Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Falta a Senha";
                    break;
                case AuthError.WrongPassword:
                    message = "Senha Errada";
                    break;
                case AuthError.InvalidEmail:
                    message = "Email Invalido";
                    break;
                case AuthError.UserNotFound:
                    message = "Conta não existe";
                    break;
            }
            warningLoginText.text = message;
            warningLoginText.CrossFadeAlpha(0f, 2f, false);
            confirmLoginText.text = "";
            emailLoginField.text = "";
            passwordLoginField.text = "";
        }
        else
        { 
            User = LoginTask.Result;
            Debug.LogFormat("Usuário logado com sucesso: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Login realizado...";
            confirmLoginText.CrossFadeAlpha(0f, 2f, false);
            var user = auth.CurrentUser.DisplayName;
            Manager.Instance.username = user;
            StartCoroutine(CarregaCena());
        }
        IEnumerator CarregaCena()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("1andar-CPD");
        }
    }

    //Validações do Cadastro
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            warningRegisterText.text = "Falta o Usuário";
            warningRegisterText.CrossFadeAlpha(0f, 2f, false);
            confirmRegisterText.text = "";
            usernameRegisterField.text = "";
            emailRegisterField.text = "";
            passwordRegisterField.text = "";
            passwordRegisterVerifyField.text = "";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Senha Inválida!";
            warningRegisterText.CrossFadeAlpha(0f, 2f, false);
            confirmRegisterText.text = "";
            usernameRegisterField.text = "";
            emailRegisterField.text = "";
            passwordRegisterField.text = "";
            passwordRegisterVerifyField.text = "";
        }
        else 
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Falha para registrar {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Cadastro falhou!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Falto o Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Falta a Senha";
                        break;
                    case AuthError.WeakPassword:
                        message = "Senha Fraca";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email já cadastrado";
                        break;
                }
                warningRegisterText.text = message;
                warningRegisterText.CrossFadeAlpha(0f, 2f, false);
                confirmRegisterText.text = "";
                usernameRegisterField.text = "";
                emailRegisterField.text = "";
                passwordRegisterField.text = "";
                passwordRegisterVerifyField.text = "";
            }
            else
            {
                User = RegisterTask.Result;

                if (User != null)
                {
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        
                        Debug.LogWarning(message: $"Falha para registrar {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Nome de usuário falhou!";
                        warningRegisterText.CrossFadeAlpha(0f, 2f, false);
                        confirmRegisterText.text = "";
                        usernameRegisterField.text = "";
                        emailRegisterField.text = "";
                        passwordRegisterField.text = "";
                        passwordRegisterVerifyField.text = "";
                    }
                    else
                    {
                        warningRegisterText.text = "";
                        confirmRegisterText.text = "Usuário Cadastrado";
                        confirmRegisterText.CrossFadeAlpha(0f, 2f, false);
                        usernameRegisterField.text = "";
                        emailRegisterField.text = "";
                        passwordRegisterField.text = "";
                        passwordRegisterVerifyField.text = "";

                    }
                }
            }
        }
    }
}
