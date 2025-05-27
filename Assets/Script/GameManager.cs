using UnityEngine;
using UnityEngine.XR.Management;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject flatPlayer;
    [SerializeField] GameObject xrRig;
    [SerializeField] GameObject flatscreenCamera;

    void Start()
    {
        detectVR();
    }

    void detectVR()
    {
        bool isVR = XRGeneralSettings.Instance.Manager.isInitializationComplete;
        xrRig.SetActive(isVR);
        flatPlayer.SetActive(!isVR);
        flatscreenCamera.SetActive(!isVR);
    }
}
