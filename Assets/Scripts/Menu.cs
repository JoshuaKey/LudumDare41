using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject menuUI;
    public GameObject helpUI;

	public void OnPlay() {
        SceneManager.LoadScene("Main");
    }

    public void OnQuit() {
        Application.Quit();
    }

    public void OnHelp() {
        helpUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void OnMenu() {
        helpUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
