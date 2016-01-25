using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField]
    private GameObject sceneCamera;
    [SerializeField]
    private GameObject UICanvas;

    public override void OnStartClient(NetworkClient client)
    {
        HideSceneCamera();
        ShowUICanvas();
    }

    public override void OnStartHost()
    {
        HideSceneCamera();
        ShowUICanvas();
    }

    public override void OnStopClient()
    {
        ShowSceneCamera();
        HideUICanvas();
    }

    public override void OnStopHost()
    {
        ShowSceneCamera();
        HideUICanvas();
    }

    private void HideSceneCamera()
    {
        if (sceneCamera)
            sceneCamera.SetActive(false);
    }

    private void ShowSceneCamera()
    {
        if (sceneCamera)
            sceneCamera.SetActive(true);
    }

    private void HideUICanvas()
    {
        if (UICanvas)
            UICanvas.SetActive(false);
    }

    private void ShowUICanvas()
    {
        if (UICanvas)
            UICanvas.SetActive(true);
    }


   
}
