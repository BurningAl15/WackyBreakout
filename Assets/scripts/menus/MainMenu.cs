using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu, helpMenu;
    
    /// <summary>
    /// Handles the on click event from the play button
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        SceneManager.LoadScene("gameplay");
    }

    /// <summary>
    /// Handles the on click event from the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

    public void ToHelpMenu()
    {
        menu.SetActive(false);
        helpMenu.SetActive(true);
    }
    
    public void ReturnToMenu()
    {
        menu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
