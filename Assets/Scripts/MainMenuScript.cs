using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void startGame()
    {

        SceneManager.LoadScene("Level 1");
    }

   public void exitGame()
    {
        Application.Quit();
    }
}
