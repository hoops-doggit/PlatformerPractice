using UnityEngine;
using System.Collections;

public class GameManagerGetter : ScriptableObject
{
    protected GameManagerGetter()
    {

    }

    public static GameManager GetManager()
    {
        var managers = Object.FindObjectsOfType<GameManager>();
        GameManager myManager;

        if(managers.Length == 0)
        {
            var GameManagerPrefab = Resources.Load("GameManager");
            var newgm = Instantiate(GameManagerPrefab) as GameObject;
            myManager = newgm.GetComponent<GameManager>();
        }
        else
        {
            myManager = managers[0];
        }

        return myManager;
    }
}
