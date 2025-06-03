using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject merchantUI;

    private void Awake()
    {
        Verification();
    }

    #region Entrer/Sortie_Magasin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur entre dans la zone du magasin");
            merchantUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur sort de la zone du magasin");
            merchantUI.SetActive(false);
        }
    }
    #endregion

    private void Verification()
    {
        if (merchantUI == null)
        {
            Debug.LogWarning($"Pour de meilleur performance assigner MerchantUI sur {TransformUtils.GetFullPath(this.transform)}");
            merchantUI = GameObject.Find("MerchantUI");
        }
        merchantUI.SetActive(false);
    }
}
