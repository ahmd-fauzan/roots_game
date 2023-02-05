using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    InputHandler inputHandler;

    [SerializeField] GameObject[] tutorials;

    private int tutorialIndex;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Tutorial") != 0) return;

        if (inputHandler == null)
            inputHandler = InputHandler.Instance;

        inputHandler.OnMovementPressed += MoveTutorial;
        inputHandler.OnMousePressed += ShootTutorial;
    }

    private void OnDisable()
    {
        if (PlayerPrefs.GetInt("Tutorial") != 0) return;

        inputHandler.OnMovementPressed -= MoveTutorial;
        inputHandler.OnMousePressed -= ShootTutorial;
    }

    private void Start()
    {
        tutorialIndex = 0;

        tutorials[tutorialIndex].SetActive(PlayerPrefs.GetInt("Tutorial") == 0);
    }

    private void MoveTutorial(float value)
    {
        if (PlayerPrefs.GetInt("Tutorial") != 0) return;

        switch (tutorialIndex)
        {
            case 0:
                if(value < 0)
                {
                    tutorials[tutorialIndex].SetActive(false);
                    tutorialIndex++;
                    tutorials[tutorialIndex].SetActive(true);
                }
                break;
            case 1:
                if(value > 0)
                {
                    tutorials[tutorialIndex].SetActive(false);
                    tutorialIndex++;
                    tutorials[tutorialIndex].SetActive(true);
                }
                break;
        }
    }

    private void ShootTutorial(Vector2 direction)
    {
        if (PlayerPrefs.GetInt("Tutorial") != 0) return;

        if(tutorialIndex == 2)
        {
            tutorials[tutorialIndex].SetActive(false);
            PlayerPrefs.SetInt("Tutorial", tutorialIndex);

            GameManager gameManager = GameManager.Instance;
            gameManager.StartGameplay();
        }
    }
}
