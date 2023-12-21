using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Photon.Pun;
using System.Collections;
using UnityEngine;

    public class PlayerCustomizer : MonoBehaviourPunCallbacks
    {
        public int camisa;
        public int cabelo;
        public int calca;
        public int chapeu;
        public int sapato;

        // Variaveis Firebase
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;

        public DatabaseReference DBreference;
        public Customize customize;

    private void Start()
    {
        if (!photonView.IsMine) { return; }
        customize = FindObjectOfType<Customize>();

        StartCoroutine(LoadCustomizePlayerCoroutine());
    }

    public void SaveRoupa() //FUN��O PARA MANDAR INFORMA��ES NO FIREBASE
    {
        SaveCustomizePlayer();
    }

    public override void OnJoinedRoom() //AO ENTRAR NO PHOTON, CARREGAR DO FIREBASE
    {
        StartCoroutine(LoadCustomizePlayerCoroutine());
    }

    public void AtualizarRoupa()
    {
        StartCoroutine (LoadCustomizePlayerCoroutine());
    }

    public IEnumerator LoadCustomizePlayerCoroutine() //PEGAR INFORMA��ES DO FIREBASE
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;

        string userId = FirebaseCORE.instance.authManager.user.UserId;

        DatabaseReference playerClothsRef = DBreference.Child("users").Child(userId).Child("PlayerClouths");

        var camisaTask = playerClothsRef.Child("camisa").GetValueAsync();
        yield return new WaitUntil(() => camisaTask.IsCompleted);
        var cabeloTask = playerClothsRef.Child("cabelo").GetValueAsync();
        yield return new WaitUntil(() => cabeloTask.IsCompleted);
        var calcaTask = playerClothsRef.Child("calca").GetValueAsync();
        yield return new WaitUntil(() => calcaTask.IsCompleted);
        var chapeuTask = playerClothsRef.Child("chapeu").GetValueAsync();
        yield return new WaitUntil(() => chapeuTask.IsCompleted);
        var sapatoTask = playerClothsRef.Child("sapato").GetValueAsync();
        yield return new WaitUntil(() => sapatoTask.IsCompleted);

        if (camisaTask.IsCompleted && cabeloTask.IsCompleted && calcaTask.IsCompleted && chapeuTask.IsCompleted && sapatoTask.IsCompleted)
        {
            // Converte os resultados das tarefas para inteiros e os atribui �s vari�veis.
            camisa = int.Parse(camisaTask.Result.Value.ToString());
            cabelo = int.Parse(cabeloTask.Result.Value.ToString());
            calca = int.Parse(calcaTask.Result.Value.ToString());
            chapeu = int.Parse(chapeuTask.Result.Value.ToString());
            sapato = int.Parse(sapatoTask.Result.Value.ToString());

            customize.MeshSelect();

            photonView.RPC("UpdatePlayerCustomization", RpcTarget.AllBuffered, camisa, cabelo, calca, chapeu, sapato);
        }
    }

    [PunRPC]
    public void UpdatePlayerCustomization(int camisa, int cabelo, int calca, int chapeu, int sapato)
    {
        this.camisa = camisa;
        this.cabelo = cabelo;
        this.calca = calca;
        this.chapeu = chapeu;
        this.sapato = sapato;

        customize.MeshSelect();
    }


    public void SaveCustomizePlayer() // SALVAR NO FIREBASE AS INFORMA��ES CONFORME APERTA BOT�ES
    {
        if (FirebaseCORE.instance.authManager.user.UserId != null)
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference playerClothsRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(userId)
                .Child("PlayerClouths");

            playerClothsRef.Child("camisa").SetValueAsync(NetworkController.instance.customize.camisa);
            playerClothsRef.Child("cabelo").SetValueAsync(NetworkController.instance.customize.cabelo);
            playerClothsRef.Child("calca").SetValueAsync(NetworkController.instance.customize.calca);
            playerClothsRef.Child("chapeu").SetValueAsync(NetworkController.instance.customize.chapeu);
            playerClothsRef.Child("sapato").SetValueAsync(NetworkController.instance.customize.sapato);
        }
    }


}
