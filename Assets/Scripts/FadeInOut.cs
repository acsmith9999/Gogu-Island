using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;
    public GameObject endMenu;
    private bool finished;

    private void Start()
    {
        finished = false;
        endMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.Quit();
                Debug.Log("quit");
            }
        }
    }
    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (levelToLoad != null)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else { return; }
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        endMenu.SetActive(true);
        finished = true;
    }
 
    public void Finished()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
}
