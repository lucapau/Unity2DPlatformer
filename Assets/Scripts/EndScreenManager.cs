using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndScreenManager : MonoBehaviour
{
    public GameObject endScreenPanel;
    public Text  scoreText;
    public Text  timeText;
    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        endScreenPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowEndScreen(int score, float time)
    {
        Debug.Log("ShowEndScreen called");
        scoreText.text = "Score: " + score.ToString();
        timeText.text = "Time: " + time.ToString("F2") + "s";

        endScreenPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    private void RestartGame()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene("MainMenuScene"); 
    }
}
