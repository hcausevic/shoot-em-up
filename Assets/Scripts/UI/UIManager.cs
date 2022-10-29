using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private PlayerMovement player;

    private void Awake()
    {
        startScreen.SetActive(true);
        player.disabled = true;
        gameOverScreen.SetActive(false);
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

    public void Quit()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
