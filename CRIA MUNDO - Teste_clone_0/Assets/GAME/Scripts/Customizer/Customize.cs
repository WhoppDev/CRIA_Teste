using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviourPun
{
    [System.Serializable]
    public enum CustomType
    {
        CAMISA,CAL�A, SAPATO, LUVA, CABELO, CHAPEU
    }

    public static Customize instance;

    public RoupasDATA assets;

    public CustomType customType;

    public SkinnedMeshRenderer camisa;
    public SkinnedMeshRenderer cal�a;
    public SkinnedMeshRenderer sapato;
    public SkinnedMeshRenderer luva;
    public SkinnedMeshRenderer cabelo;
    public SkinnedMeshRenderer chapeu;

    private void Start()
    {
        instance = this;
    }

    public void MeshSelect()
    {
        cabelo.sharedMesh = assets.cabelo[NetworkController.instance.customize.cabelo];
        camisa.sharedMesh = assets.camisa[NetworkController.instance.customize.camisa];
        cal�a.sharedMesh = assets.cal�a[NetworkController.instance.customize.calca];
        sapato.sharedMesh = assets.sapato[NetworkController.instance.customize.sapato];
        chapeu.sharedMesh = assets.chapeu[NetworkController.instance.customize.chapeu];
    }

    [PunRPC]
    public void EscolherCor(Image botaoImagem)
    {
        switch (customType)
        {
            case CustomType.CAMISA:
                Material camisaMaterial = camisa.material;
                camisaMaterial.color = botaoImagem.color;
                break;
            case CustomType.CABELO:
                Material cabeloMaterial = cabelo.material;
                cabeloMaterial.color = botaoImagem.color;
                break;
            case CustomType.CAL�A:
                Material cal�aMaterial = cal�a.material;
                cal�aMaterial.color = botaoImagem.color;
                break;
            case CustomType.CHAPEU:
                Material chapeuMaterial = chapeu.material;
                chapeuMaterial.color = botaoImagem.color;
                break;
            case CustomType.SAPATO:
                Material sapatoMaterial = sapato.material;
                sapatoMaterial.color = botaoImagem.color;
                break;
        }



    }
}
