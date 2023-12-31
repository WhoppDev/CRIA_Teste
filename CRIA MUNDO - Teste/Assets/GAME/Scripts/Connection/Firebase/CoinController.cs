using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public int coinsValue;

    public TMP_Text coinValue;

    public DatabaseReference DBreference;

    private void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;

        StartCoroutine(CheckAuthenticationAndLoadCoins());
    }

    private IEnumerator CheckAuthenticationAndLoadCoins()
    {
        yield return new WaitUntil(() => auth.CurrentUser != null); // Espera pela autentica��o do usu�rio.

        if (auth.CurrentUser != null)
        {
            Debug.Log("Usu�rio est� autenticado com UID: " + auth.CurrentUser.UserId);
            StartCoroutine(LoadPlayerCoins());
        }
        else
        {
            Debug.LogWarning("Usu�rio n�o est� autenticado!");
        }
    }

    private IEnumerator LoadPlayerCoins()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;
        var getPlayerCoinsTask = DBreference.Child("users").Child(userId).Child("PlayerCoins").GetValueAsync();

        yield return new WaitUntil(() => getPlayerCoinsTask.IsCompleted);

        if (getPlayerCoinsTask.IsFaulted)
        {
            // Handle the error...
            Debug.LogError(getPlayerCoinsTask.Exception);
        }
        else if (getPlayerCoinsTask.IsCompleted)
        {
            DataSnapshot snapshot = getPlayerCoinsTask.Result;
            if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int coins))
            {
                CORE.instance.status.playerCoin = coins;
                coinValue.text = coins.ToString();
            }
            else
            {
                Debug.Log("PlayerCoins not found or invalid format.");
            }
        }
    }


    // M�todo para atualizar as moedas no Firebase
    public void UpdatePlayerCoinsInFirebase(int coins)
        {
            if (FirebaseCORE.instance.authManager.user.UserId != null)
            {
                string userId = FirebaseCORE.instance.authManager.user.UserId;
                DatabaseReference playerCoinsRef = FirebaseDatabase.DefaultInstance.RootReference
                    .Child("users")
                    .Child(userId)
                    .Child("PlayerCoins");

                // Atualize esta linha para passar o valor inteiro das moedas
                playerCoinsRef.SetValueAsync(coins);
            }
            else
            {
                Debug.LogWarning("User not authenticated. Cannot update player coins.");
            }
        }
}
