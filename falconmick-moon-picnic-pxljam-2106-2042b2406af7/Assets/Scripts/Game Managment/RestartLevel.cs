using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class RestartLevel : ExecutableComponent
{
    public override void Execute(object args = null)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
