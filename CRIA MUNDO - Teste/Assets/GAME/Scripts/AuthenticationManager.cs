using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseReference database;

    // Substitua pelos dados do usu�rio de teste
    private string testEmail = "test@example.com";
    private string testPassword = "yourpassword";

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            auth = FirebaseAuth.DefaultInstance;
            database = FirebaseDatabase.DefaultInstance.RootReference;

            TestFirebaseAuth();
        });
    }

    void TestFirebaseAuth()
    {
        auth.SignInWithEmailAndPasswordAsync(testEmail, testPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync foi cancelado.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encontrou um erro: " + task.Exception);
                return;
            }

            // Corre��o: Agora o tipo correto AuthResult � atribu�do e depois usamos a propriedade User
            AuthResult result = task.Result;
            FirebaseUser newUser = result.User;
            Debug.LogFormat("Usu�rio autenticado com sucesso: {0} ({1})", newUser.DisplayName, newUser.UserId);

            // Continua��o do c�digo...
        });
    }


    void TestWriteToDatabase()
    {
        database.Child("users").Child(auth.CurrentUser.UserId).Child("test").SetValueAsync("Hello Firebase").ContinueWith(writeTask =>
        {
            if (writeTask.IsFaulted)
            {
                Debug.LogError("Erro ao escrever no Firebase Database");
            }
            else if (writeTask.IsCompleted)
            {
                Debug.Log("Dados escritos com sucesso no Firebase Database");
            }
        });
    }
}
