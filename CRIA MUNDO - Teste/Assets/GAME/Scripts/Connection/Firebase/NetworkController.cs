using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController instance;
    public PlayerCustomizer customize;
    public CoinController coinController;
    public ScoreController scoreController;

    public GameObject player;  // Informa��o retirada do script PlayerInfo qunado o player � instanciado

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
