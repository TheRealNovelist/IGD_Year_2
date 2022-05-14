using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obsolete
{
    public class Room_HUB : Room_Framework
    {
        public static int HUBLevel = 0;
        private GameManager gameManager => FindObjectOfType<GameManager>();
        private HUBUI hubUI => FindObjectOfType<HUBUI>();

        //private BuildGrid grid => GameObject.Find("_BuildGrid").GetComponent<BuildGrid>();

        [System.Serializable]
        public struct LevelSetting
        {
            public Vector2Int gridSize;
            public int costToLevelUp;
        }

        public LevelSetting[] levelSettings;

        //Reset default values
        public void Reset()
        {
            roomSize = new Vector2Int(2, 2);
            buildType = BuildType.HUB;
            cost = 0;
        }

        public void OnMouseDown()
        {
            hubUI.OpenPanel(this);
        }

        public void HUBLevelUp()
        {
            if (!IsHUBMaxLevel())
            {
                if (gameManager.MoneyManager.PayMoney(levelSettings[HUBLevel + 1].costToLevelUp))
                {
                    HUBLevel++;
                    Debug.Log("HUB: Level Up!");
                    LevelSetting currentLevelSetting = levelSettings[HUBLevel];
                    //grid.ChangeGridSize(currentLevelSetting.gridSize.x, currentLevelSetting.gridSize.y);
                    hubUI.PopulatePanel(this);
                    gameManager.ReputationManager.AddExperience(200);
                }
            }
        }

        public bool IsHUBMaxLevel()
        {
            if (levelSettings.Length - 1 <= HUBLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetNextLevelRequirement()
        {
            if (!IsHUBMaxLevel())
            {
                return levelSettings[HUBLevel].costToLevelUp.ToString();
            }
            else
            {
                return "MAXED";
            }
        }
    }

}