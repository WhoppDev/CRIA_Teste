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


    [PunRPC]
    public void TrocarRoupaRPC(string novaCamisaMesh, string novaCalcaMesh, string novaLuvaMesh, string novoSapatoMesh, string novoCabeloMesh, string novoChapeuMesh)
    {


        if (!string.IsNullOrEmpty(novaCamisaMesh)) camisa.sharedMesh = FindMeshByName(novaCamisaMesh);
        if (!string.IsNullOrEmpty(novaCalcaMesh)) cal�a.sharedMesh = FindMeshByName(novaCalcaMesh);
        if (!string.IsNullOrEmpty(novoSapatoMesh)) sapato.sharedMesh = FindMeshByName(novoSapatoMesh);
        if (!string.IsNullOrEmpty(novaLuvaMesh)) luva.sharedMesh = FindMeshByName(novaLuvaMesh);
        if (!string.IsNullOrEmpty(novoCabeloMesh)) cabelo.sharedMesh = FindMeshByName(novoCabeloMesh);
        if (!string.IsNullOrEmpty(novoChapeuMesh)) chapeu.sharedMesh = FindMeshByName(novoChapeuMesh);
    }

    private Mesh FindMeshByName(string meshName)
    {
        Mesh[] meshes = Resources.FindObjectsOfTypeAll<Mesh>();
        foreach (Mesh mesh in meshes)
        {
            if (mesh.name == meshName)
            {
                return mesh;
            }
        }
        return null;
    }

    public void MeshSelect()
    {
        cabelo.sharedMesh = assets.cabelo[CORE.instance.customize.cabelo];
        camisa.sharedMesh = assets.camisa[CORE.instance.customize.camisa];
        cal�a.sharedMesh = assets.cal�a[CORE.instance.customize.cal�a];
        sapato.sharedMesh = assets.sapato[CORE.instance.customize.sapato];
        luva.sharedMesh = assets.luva[CORE.instance.customize.luva];
        chapeu.sharedMesh = assets.chapeu[CORE.instance.customize.chapeu];
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
