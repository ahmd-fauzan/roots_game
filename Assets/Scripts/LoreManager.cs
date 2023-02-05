using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoreManager : MonoBehaviour
{
    [SerializeField]
    private string[] lores;

    [SerializeField] private TextMeshProUGUI loreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowLore());
    }

    private IEnumerator ShowLore()
    {
        Color textColor = loreText.color;

        foreach(string str in lores)
        {
            loreText.text = str;

            for(float i = 0; i <= 1; i = i + .05f)
            {
                textColor.a = i;
                loreText.color = textColor;
                yield return new WaitForSeconds(.05f);
            }

            for (float i = 1; i > 0; i = i - .05f)
            {
                textColor.a = i;
                loreText.color = textColor;
                yield return new WaitForSeconds(.05f);
            }

            textColor.a = 0;
            loreText.color = textColor;

            yield return new WaitForSeconds(.5f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
