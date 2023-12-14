    using Firebase;
    using Firebase.Auth;
    using Firebase.Database;
    using UnityEngine;

    public class PlayerCustomizer : MonoBehaviour
    {
        public int camisa;
        public int cal�a;
        public int luva;
        public int sapato;
        public int cabelo;
        public int chapeu;

        // Variaveis Firebase
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;

        public DatabaseReference DBreference;

        private void Start()
        {
            LoadCustomizePlayer();
        }

        public void AtualizarRoupa()
        {
            Customize.instance.MeshSelect();
            SaveCustomizePlayer();
        }

    public void LoadCustomizePlayer()
    {
        Debug.Log("Iniciando LoadCustomizePlayer");
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = NetworkController.instance.coinController.DBreference;

        string userId = FirebaseCORE.instance.authManager.user.UserId;
        Debug.Log("UserId: " + userId);

        DBreference.Child("users").Child(userId).Child("PlayerClouths").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Erro ao carregar customiza��o: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log("Dados encontrados: " + snapshot.GetRawJsonValue());
                    string jsonData = snapshot.GetRawJsonValue();
                    PlayerCustomizationData customizationData = JsonUtility.FromJson<PlayerCustomizationData>(jsonData);

                    Debug.Log("Iniciar o Apply");

                    ApplyCustomization(customizationData);
                }
                else
                {
                    Debug.Log("Dados de customiza��o n�o encontrados.");
                }
            }
        });
    }


    private void ApplyCustomization(PlayerCustomizationData data)
    {
        Debug.Log($"Aplicando customiza��o com os seguintes �ndices: Camisa {data.camisa}, Cabelo {data.cabelo}, Cal�a {data.cal�a}, Chap�u {data.chapeu}, Sapato {data.sapato}");

            NetworkController.instance.customize.camisa = data.camisa;
            NetworkController.instance.customize.cabelo = data.cabelo;
            NetworkController.instance.customize.cal�a = data.cal�a;
            NetworkController.instance.customize.chapeu = data.chapeu;
            NetworkController.instance.customize.sapato = data.sapato;

            // Confirme os valores ap�s a aplica��o.
            Debug.Log($"Valores ap�s aplica��o: Camisa {NetworkController.instance.customize.camisa}, Cabelo {NetworkController.instance.customize.cabelo}");
    }


    public class PlayerCustomizationData
        {
            public int camisa;
            public int cabelo;
            public int cal�a;
            public int chapeu;
            public int sapato;
        }

        public void SaveCustomizePlayer()
        {
            if (FirebaseCORE.instance.authManager.user.UserId != null)
            {
                string userId = FirebaseCORE.instance.authManager.user.UserId;
                DatabaseReference playerCustomizationRef = FirebaseDatabase.DefaultInstance.RootReference
                    .Child("users")
                    .Child(userId)
                    .Child("PlayerClouths");

                PlayerCustomizationData data = new PlayerCustomizationData
                {
                    camisa = NetworkController.instance.customize.camisa,
                    cabelo = NetworkController.instance.customize.cabelo,
                    cal�a = NetworkController.instance.customize.cal�a,
                    chapeu = NetworkController.instance.customize.chapeu,
                    sapato = NetworkController.instance.customize.sapato
                };

                string jsonData = JsonUtility.ToJson(data);
                playerCustomizationRef.SetValueAsync(jsonData);
            }
            else
            {
                Debug.LogWarning("User not authenticated. Cannot update player customization.");
            }
        }

    }
