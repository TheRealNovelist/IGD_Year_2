using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildUI : MonoBehaviour
{
    public GameObject buildPanel;
    public BuildGrid grid;

    private static bool isBuildMenu = false;

    [Header("Build Button")]
    public GameObject buildButton;
    public GameObject returnButton;

    [Header("Component")]
    public BuildManager buildManager;

    public static bool IsBuildMenuActive()
    {
        return isBuildMenu;
    }

    public void ChooseRoom(GameObject room)
    {
        buildManager.SelectRoom(room);

        returnButton.SetActive(true);

        grid.ToggleGrid(true);

        buildPanel.SetActive(false);
    }

    public void OpenBuildMenu()
    {
        buildButton.SetActive(false);
        buildPanel.SetActive(true);
        isBuildMenu = true;
    }

    public void ReturnToGame()
    {
        buildButton.SetActive(true);
        buildPanel.SetActive(false);
        isBuildMenu = false;
    }

    public void ReturnToBuildMenu()
    {
        returnButton.SetActive(false);
        grid.ToggleGrid(false);
        buildPanel.SetActive(true);
    }
}
