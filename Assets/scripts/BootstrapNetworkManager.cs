using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapNetworkManager : MonoBehaviour
{
    private static BootstrapNetworkManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
