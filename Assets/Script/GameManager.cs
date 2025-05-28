using UnityEngine;
using UnityEngine.XR.Management;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject flatPlayer;
    [SerializeField] GameObject xrRig;
    [SerializeField] GameObject flatscreenCamera;

    [HideInInspector]
    public bool isVR;
    private GameObject cannonOrientation;

    private void Awake()
    {
        #region verification
        if (flatPlayer == null || xrRig == null || flatscreenCamera == null)
        {
            if (flatPlayer == null && xrRig == null && flatscreenCamera == null)
            {
                Debug.LogError($"Mettre les références pour flatPlayer, xrRig et flatscreenCamera sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else if (flatPlayer == null)
            {
                Debug.LogError($"Mettre la référence pour flatPlayer sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else if (xrRig == null)
            {
                Debug.LogError($"Mettre la référence pour xrRig sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else
            {
                Debug.LogError($"Mettre la référence pour flatscreenCamera sur {TransformUtils.GetFullPath(this.transform)}");
            }
        }
        #endregion
    }
    void Start()
    {
        detectVR();
    }

    void detectVR()
    {
        isVR = XRGeneralSettings.Instance.Manager.isInitializationComplete;
        xrRig.SetActive(isVR);
        flatPlayer.SetActive(!isVR);
        flatscreenCamera.SetActive(!isVR);
    }

    public void setCannonOrientation(GameObject cannonOrientation)
    {
        this.cannonOrientation = cannonOrientation;
    }

    public GameObject getCannonOrientation()
    {
        return cannonOrientation;
    }
}
