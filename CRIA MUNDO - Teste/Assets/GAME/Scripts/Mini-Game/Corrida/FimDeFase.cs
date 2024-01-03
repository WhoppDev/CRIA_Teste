using UnityEngine;
using UnityEngine.UI;

public class FimDeFase : MonoBehaviour
{
    public GameObject endLevelHUD;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        { 
            endLevelHUD.SetActive(true);
            Pontua��oFinal();
        }
    }

    public void Pontua��oFinal()
    {
        if(NetworkController.instance.scoreController.currentScorePoint > NetworkController.instance.scoreController.scorePoint)
        {
            NetworkController.instance.scoreController.UpdatePlayerPointsInFirebase(NetworkController.instance.scoreController.currentScorePoint);
        }
    }

}
