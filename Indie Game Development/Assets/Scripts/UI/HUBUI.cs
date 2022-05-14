using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Obsolete
{
    public class HUBUI : MonoBehaviour
    {
        private GameManager gameManager => FindObjectOfType<GameManager>();
        //[SerializeField] private BuildGrid grid => FindObjectOfType<BuildGrid>();
        [SerializeField] private Room_HUB room;

        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI gridWidth;
        [SerializeField] private TextMeshProUGUI gridHeight;
        [SerializeField] private TextMeshProUGUI upgradeRequirement;

        private void Update()
        {
            //gridWidth.text = grid.Width.ToString();
            //gridHeight.text = grid.Height.ToString();

            if (room != null)
            {
                upgradeRequirement.text = room.GetNextLevelRequirement();
            }
        }

        public void OpenPanel(Room_HUB room)
        {
            // if (!BuildUI.IsBuildMenuActive())
            // {
            //     gameManager.DismissAllPanel();
            //
            //     panel.SetActive(true);
            //
            //     PopulatePanel(room);
            // }
        }

        public void PopulatePanel(Room_HUB room)
        {
            this.room = room;

            levelText.text = Room_HUB.HUBLevel.ToString();
        }

        public void ClosePanel()
        {
            panel.SetActive(false);
        }

        public void UpgradeHUBButton()
        {
            room.HUBLevelUp();
        }
    }

}