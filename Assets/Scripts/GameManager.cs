using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas mainCanvas, levelCanvas, optionsCanvas, winCanvas, hudCanvas, shopCanvas;
    public static bool isPaused = false;
    public static GameManager instance;
    public bool levelComplete = false;
    public bool reachedFinish = false;
    public bool includesLogin = false;

    Animator anim;
    GameObject[] stars;
    public GameObject fireworks;

    FacebookController facebookController;
    GooglePlayGamesController googleController;

    GameObject fbLoginButton, fbLogoutButton, googleLoginButton, googleLogoutButton, achievementsButton;

    public void ShowScore()
    {
        if (winCanvas.enabled)
        {
            //Debug.Log("Showing score");
            //TextMeshProUGUI starText = GameObject.Find("StarText").GetComponent<TextMeshProUGUI>();
            //starText.SetText(Stats.stars.ToString());
            //GameObject[] stars = GameObject.FindGameObjectsWithTag("UI Star");

            //for (int i = 0; i < Stats.instance.stars; i++)
            //{
            //    Debug.Log("Set to true: " + stars[i].name);
            //    stars[i].SetActive(true);
            //}

            StartCoroutine(ShowStars());
            int currentMoves = Stats.instance.moves;
            TextMeshProUGUI moveText = GameObject.Find("MoveText").GetComponent<TextMeshProUGUI>();
            moveText.SetText(currentMoves.ToString());

            string bestKey = "Best" + SceneManager.GetActiveScene().buildIndex.ToString();
            int bestMoves = currentMoves;
            if (PlayerPrefs.HasKey(bestKey))
            {
                bestMoves = PlayerPrefs.GetInt(bestKey); // returns 0 if not set
                if (currentMoves < bestMoves)
                {
                    bestMoves = currentMoves;
                    PlayerPrefs.SetInt(bestKey, bestMoves);
                }
            }
            else
            {
                PlayerPrefs.SetInt(bestKey, bestMoves);
            }
            TextMeshProUGUI bestText = GameObject.Find("BestText").GetComponent<TextMeshProUGUI>();
            bestText.SetText(bestMoves.ToString());
        }
    }

    public IEnumerator ShowStars()
    {
        for (int i = 0; i < Stats.instance.stars; i++)
        {
            //Debug.Log("Set to true: " + stars[i].name);
            stars[i].SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }

    public void ShowHUDScore()
    {
        TextMeshProUGUI levelText = GameObject.Find("LevelTextHUD").GetComponent<TextMeshProUGUI>();
        levelText.SetText(SceneManager.GetActiveScene().buildIndex.ToString());

        TextMeshProUGUI moveText = GameObject.Find("MoveTextHUD").GetComponent<TextMeshProUGUI>();
        moveText.SetText(Stats.instance.moves.ToString());
    }

    public void ResetLevel()
    {
        //isPaused = false;
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); // Main menu should be at index 0 in build settings
    }

    public void ShowLevels()
    {
        mainCanvas.enabled = false;
        levelCanvas.enabled = true;
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ShowHideOptions()
    {
        //Debug.Log("ShowHide OPtions");
        if (mainCanvas)
            mainCanvas.enabled = false;
        if (!optionsCanvas.enabled)
        {
            optionsCanvas.enabled = true;
            if (includesLogin)
            {
                EnableDisableLoginButtons();
            }
        }
        else
            optionsCanvas.enabled = false;
    }

    public void EnableDisableLoginButtons()
    {
        //GameObject fbLoginButton = GameObject.Find("FB Login");
        //GameObject fbLogoutButton = GameObject.Find("FB Logout");
        //GameObject googleLoginButton = GameObject.Find("Google Login");
        //GameObject googleLogoutButton = GameObject.Find("Google Logout");

        if (fbLoginButton != null && fbLogoutButton != null)
        {
            if (facebookController.IsLoggedIn())
            {
                fbLoginButton.SetActive(false);
                fbLogoutButton.SetActive(true);
                SetUserName(facebookController.GetUserInfo().Name);
            }
            else
            {
                fbLoginButton.SetActive(true);
                fbLogoutButton.SetActive(false);
                SetUserName("Guest User");
            }
        }

        if (googleLoginButton != null && googleLogoutButton != null)
        {
            // GetUserName returns empty string if not logged in
            if (!string.IsNullOrEmpty(googleController.GetUserName()))
            {
                googleLoginButton.SetActive(false);
                googleLogoutButton.SetActive(true);
                achievementsButton.SetActive(true);
                SetUserName(googleController.GetUserName());
            }
            else
            {
                googleLoginButton.SetActive(true);
                googleLogoutButton.SetActive(false);
                achievementsButton.SetActive(false);
                SetUserName("Guest User");
            }
        }
    }

    public void SetUserName(string name)
    {
        TextMeshProUGUI username = GameObject.Find("Username").GetComponent<TextMeshProUGUI>();
        // First preference Google Play username
        // Second preference Facebook username
        if (name != string.Empty)
        {
            username.SetText(name);
        }
        else
        {
            // Default username - Guest User
            username.SetText("Guest User");
        }
    }

    public void LevelCompleted()
    {
        // Only run this once by checking for the value of levelComplete
        if (!reachedFinish)
        {
            reachedFinish = true;
            StartCoroutine(ShowFireworks());
            FindObjectOfType<AudioManager>().Play("Level Victory");
        }
    }

    public IEnumerator ShowFireworks()
    {
        //Debug.Log("Started Fireworks");
        var finishBlock = GameObject.FindGameObjectWithTag("Finish");
        var fireworksSlots = GameObject.FindGameObjectsWithTag("Fireworks Slot");
        foreach (var fireworksSlot in fireworksSlots)
        {
            Instantiate(fireworks, fireworksSlot.transform);
        }
        yield return new WaitForSeconds(5);
        ShowWinCanvas();
    }

    public void ShowWinCanvas()
    {
        if (!winCanvas.enabled)
        {
            //Instantiate(fireworks);
            
            
            winCanvas.enabled = true;
            //isPaused = true;
            //Time.timeScale = 0f;
            ShowScore();
        }
    }

    public void ShowShop()
    {
        mainCanvas.enabled = false;
        shopCanvas.enabled = true;
    }

    public void Awake()
    {
        // Set FB and Google Play Games controllers
        if (includesLogin)
        {
            facebookController = GetComponent<FacebookController>();
            googleController = GetComponent<GooglePlayGamesController>();

            facebookController.OnInitialize();
            googleController.OnInitialize();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start Game manager");
        if (instance == null)
        {
            instance = gameObject.GetComponent<GameManager>();
        }
        if (mainCanvas)
        {
            anim = mainCanvas.GetComponentInChildren<Animator>();
            mainCanvas.enabled = true;
        }
        if (levelCanvas)
        {
            levelCanvas.enabled = false;
        }
        if (optionsCanvas)
        {
            optionsCanvas.enabled = false;
        }
        if (winCanvas)
        {
            stars = GameObject.FindGameObjectsWithTag("UI Star");
            //Debug.Log("Stars length: " + stars.Length);
            foreach (GameObject star in stars)
            {
                star.SetActive(false);
            }
            winCanvas.enabled = false;
        }
        if (hudCanvas)
        {
            hudCanvas.enabled = true;
        }
        if(shopCanvas)
        {
            shopCanvas.enabled = false;
        }

        if (includesLogin)
        {
            PlayerPrefs.DeleteAll();
            fbLoginButton = GameObject.Find("FB Login");
            fbLogoutButton = GameObject.Find("FB Logout");
            googleLoginButton = GameObject.Find("Google Login");
            googleLogoutButton = GameObject.Find("Google Logout");
            achievementsButton = GameObject.Find("Achievements");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hudCanvas)
        {
            ShowHUDScore();
        }

        if (includesLogin)
        {
            EnableDisableLoginButtons();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayerData();
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();

        if (playerData != null)
        {
            Debug.Log("Player Car: " + playerData.playerCar);
        }
    }
}
