using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    // Components
    [SerializeField]  private Animator shady;
    [SerializeField]  private Image bar;
    [SerializeField]  private Image barArticle;
    [SerializeField]  private Sprite[] loadingPics;
    [SerializeField]  private Text waitString;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        instance = this;

    }

    public void LoadScene(string sceneName, bool isDirt, bool isWaitEvent)
    {
        // Get loading picture.
        System.Random rand = new System.Random();
        var index = rand.Next(0, loadingPics.Length);
        shady.GetComponent<Image>().sprite = loadingPics[index];

        // Open Shady and Componet.
        if (!shady.isActiveAndEnabled)
        {
            shady.gameObject.SetActive(true);
        }
            
        
        // Direct Load Scene
        if (isDirt)
        {
            SceneManager.LoadScene(sceneName);
            shady.SetTrigger("transfer");
            return;
        }

        // If not direct, active laoding bar.
        bar.gameObject.SetActive(true);

        StartCoroutine(LoadingState(sceneName, isWaitEvent));
    }

    IEnumerator LoadingState(string sceneName, bool isWaitEvent)
    {
        float effectVar = 0; float previousVar = 0;

        // Setting AsyncLoad.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        // Enter loop for check loading process rate.
        while(true)
        {
            // Calculate update rate.
            if (effectVar < previousVar)
            {
                if (previousVar - effectVar > 0.03f)
                    effectVar = Mathf.Clamp(effectVar + 0.03f, 0, 0.9f);
                else
                    effectVar = Mathf.Clamp(effectVar + 0.01f, 0, 0.9f);
            }

            // Get async's process rate and update article.
            previousVar = async.progress;
            barArticle.fillAmount = effectVar;

            if (effectVar >= 0.9f)
            {
                barArticle.fillAmount = 1.0f;
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        if (!isWaitEvent)
        {
            barArticle.fillAmount = 0.0f;
            shady.SetTrigger("transfer");
            async.allowSceneActivation = true;
            bar.gameObject.SetActive(false);
        }
        else
        {
            // if need wait another player, close loading bar and view text.
            barArticle.fillAmount = 0.0f;
            bar.gameObject.SetActive(false);
            waitString.gameObject.SetActive(true);
            /*
                push loading finish request. 
            */
        }
        yield return null;
    }



}