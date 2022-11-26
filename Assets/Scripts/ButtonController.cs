using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ButtonClickEvent();
public delegate void LevelButtonClickEvent(int level);

public class ButtonController : MonoBehaviour
{
    public event ButtonClickEvent OnButtonClicked;
    public event LevelButtonClickEvent OnLevelButtonClicked;

    public int levelToLoad;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(ButtonHandler);
        //Debug.Log("LevletoLoad: " + levelToLoad);
    }

    private void ButtonHandler()
    {
        if (CompareTag("Level Button"))
        {
            // Main menu level buttons use this tag
            OnLevelButtonClicked = GameManager.instance.LoadLevel;
        }
        else
        {
            switch (name)
            {
                case "Play":
                    OnButtonClicked = GameManager.instance.ShowLevels;
                    break;
                case "Options":
                case "Return":
                    OnButtonClicked = GameManager.instance.ShowHideOptions;
                    break;
                case "Home":
                    OnButtonClicked = GameManager.instance.LoadMainMenu;
                    break;
                case "Restart":
                    OnButtonClicked = GameManager.instance.ResetLevel;
                    break;
                case "Next":
                    OnLevelButtonClicked = GameManager.instance.LoadLevel;
                    break;
                case "Shop":
                    OnButtonClicked = GameManager.instance.ShowShop;
                    break;
            }
        }

        FindObjectOfType<AudioManager>().Play("Button Click");
        StartCoroutine(AnimateButton());
    }

    private IEnumerator AnimateButton()
    {
        // Wait until button pressed animation has completed
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        
        if (OnButtonClicked != null)
        {
            OnButtonClicked.Invoke();
        }

        if(OnLevelButtonClicked!=null)
        {
            OnLevelButtonClicked.Invoke(levelToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}