using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //-------------------------------------------------
    public void GoToLevelOneScene()
    {
        SceneManager.LoadScene("Level1One", LoadSceneMode.Single);
    }


    //-------------------------------------------------
    public void GoToLevelThreeScene()
    {
        SceneManager.LoadScene("Level3Three", LoadSceneMode.Single);
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene("MenuTestScene", LoadSceneMode.Single);
    }


}
