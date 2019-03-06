using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitApplication : MonoBehaviour
{
    private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

    public float PauseUntilQuit = 5f;

    void Awake()
    {
        showPanels = GetComponent<ShowPanels>();
    }

	public void Quit()
	{

        //Hide the main menu UI element
        showPanels.HideMenu();
        SceneManager.LoadScene(2);
	    StartCoroutine(QuitInABit());
	}

    private IEnumerator QuitInABit()
    {
        yield return new WaitForSeconds(PauseUntilQuit);
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
