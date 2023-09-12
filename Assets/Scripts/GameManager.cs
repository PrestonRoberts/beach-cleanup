using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Trash
    [SerializeField] private TrashSpawning allTrash;
    [SerializeField] private TMP_Text bottleText;
    [SerializeField] private TMP_Text trashBagText;
    [SerializeField] private TMP_Text barrelText;

    // UI
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text finalTimeText;

    [SerializeField] private GameObject pauseMenu;

    private int maxBottleCount;
    private int maxTrashBagCount;
    private int maxBarrelCount;

    private int bottleCount;
    private int trashBagCount;
    private int barrelCount;

    // Player
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private MouseLook mouseLook;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Unpause();
            }
            else
            {
                // Show pause menu
                pauseMenu.SetActive(true);

                // Disable player
                DisablePlayer();

                // Stop timer
                timer.StopTimer();
            }
        }
    }

    // Unpause game
    public void Unpause()
    {
        // Hide pause menu
        pauseMenu.SetActive(false);

        // Enable player
        EnablePlayer();

        // Start timer
        timer.StartTimer();
    }

    // Start the game
    public void StartGame()
    {
        // Get trash counts
        maxBottleCount = allTrash.bottleCount;
        maxTrashBagCount = allTrash.trashBagCount;
        maxBarrelCount = allTrash.barrelCount;

        // Set trash counts
        bottleCount = maxBottleCount;
        trashBagCount = maxTrashBagCount;
        barrelCount = maxBarrelCount;

        // Spawn trash
        allTrash.StartGame();

        // Restart timer
        timer.RestartTimer();

        // Hide win screen
        winScreen.SetActive(false);

        // Hide pause menu
        pauseMenu.SetActive(false);

        // Move player to start position
        player.transform.position = startPosition.transform.position;

        // Enable player movement
        EnablePlayer();

        // Update text for trash counts
        UpdateTexts();
    }

    // Update text for trash counts
    void UpdateTexts()
    {
        bottleText.text = bottleCount + "/" + maxBottleCount;
        trashBagText.text = trashBagCount + "/" + maxTrashBagCount;
        barrelText.text = barrelCount + "/" + maxBarrelCount;
    }

    public void CollectTrash(string trashName)
    {
        if(trashName == "Bottle")
        {
            bottleCount--;
        }
        else if(trashName == "Trashbag")
        {
            trashBagCount--;
        }
        else if(trashName == "Barrel")
        {
            barrelCount--;
        }

        UpdateTexts();
        CheckWin();
    }

    void CheckWin()
    {
        if(bottleCount == 0 && trashBagCount == 0 && barrelCount == 0)
        {
            // Show win screen
            winScreen.SetActive(true);

            // Stop timer
            timer.StopTimer();

            // Update final time text
            finalTimeText.text = "in " + timer.convertTime();

            // Disable player
            DisablePlayer();
        }
    }

    // Disable player movements
    public void DisablePlayer()
    {
        // Disable player movement
        playerMovement.DisableMovement();

        // Disable mouse look
        mouseLook.DisableLook();
    }

    // Enable player movements
    public void EnablePlayer()
    {
        // Enable player movement
        playerMovement.EnableMovement();

        // Enable mouse look
        mouseLook.EnableLook();
    }

    // Play again button
    public void PlayAgain()
    {
        // Start game
        StartGame();
    }

    // Main menu button
    public void MainMenu()
    {
        // Load main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
