using UnityEngine;

public interface ICaptureMinigame
{
    void Init(CaptureMinigameData data, GameObject ghost);
    bool IsComplete();
}
