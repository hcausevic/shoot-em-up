using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private PlayerMovement player;
    
    private GemManager _gemManager;
    
    [DllImport("__Internal")]
    private static extern void CloseWindow();

    private void Awake()
    {
        startScreen.SetActive(true);
        player.disabled = true;
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        _gemManager = FindObjectOfType<GemManager>();
    }

    private void Update()
    {
        if (startScreen.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) | Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            startScreen.SetActive(false);
            player.disabled = false;
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckIfWin()
    {
        if (GameObject.FindGameObjectsWithTag("Gem").Length == 0)
        {
            player.Win();
            winScreen.SetActive(true);
        }
        else
        {
            _gemManager.DisplayGemsNotPickedMessage();
        }
    }

    public void Quit()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            CloseWindow();
        }
        else
        {
            Application.Quit();
        }
        
    }
}
