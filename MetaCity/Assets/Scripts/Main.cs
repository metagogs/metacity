using UnityEngine;
public class Main : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnGameLoaded()
    {
        Debug.Log("Game Start...");

        NetworkManager.Instance().Init();
    }
}