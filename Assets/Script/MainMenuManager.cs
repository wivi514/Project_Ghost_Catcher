using UnityEngine;
using UnityEngine.XR.Management;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Camera vrCamera;
    [SerializeField] Camera flatCamera;

    private void Awake()
    {
        VRVerification();
    }

    private void VRVerification()
    {
        if (vrCamera == null || flatCamera == null)
        {
            if (vrCamera == null && flatCamera == null)
            {
                Debug.LogError($"Mettre r�f�rence � vrCamera et flatCamera sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else if (vrCamera == null)
            {
                Debug.LogWarning($"Mettre r�f�rence � vrCamera sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else
            {
                Debug.LogWarning($"Mettre r�f�rence � flatCamera sur {TransformUtils.GetFullPath(this.transform)}");
            }
        }
        else
        {
            bool isVR = XRGeneralSettings.Instance.Manager.isInitializationComplete;
            if (isVR)
            {
                flatCamera.gameObject.SetActive(false);
            }
            else
            {
                vrCamera.gameObject.SetActive(false);
            }
        }
    }
}
