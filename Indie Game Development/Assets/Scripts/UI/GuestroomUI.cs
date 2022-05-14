using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Obsolete
{
    public class GuestroomUI : MonoBehaviour
    {
        public GameManager gameManager => FindObjectOfType<GameManager>();

        public Room_Guest currentGuestRoom;

        [SerializeField] private GameObject panel;

        [Header("Guest Panel")]
        [SerializeField] private GameObject guestRoomPanel;
        [Space]
        [SerializeField] private TextMeshProUGUI guestNameText;
        [SerializeField] private TextMeshProUGUI guestCurrentActivityText;
        [SerializeField] private TextMeshProUGUI guestPayoutText;
        [Space]
        [SerializeField] private TextMeshProUGUI guestCurrentStayTimeText;
        [SerializeField] private Slider guestStayTimeSlider;

        [Header("Room Limit and Janitor")]
        [SerializeField] private TextMeshProUGUI roomLimitText;
        [SerializeField] private TextMeshProUGUI janitorAmountText;
        [SerializeField] private Button cleanRoomButton;
        [Space]
        [SerializeField] private Slider cleaningSlider;

        [Header("Money")]
        [SerializeField] private TextMeshProUGUI totalMoneyText;

        public void OpenPanel(Room_Guest room)
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

        public void PopulatePanel(Room_Guest room)
        {
            guestRoomPanel.SetActive(true);

            currentGuestRoom = room;

            guestNameText.text = room.currentGuestInRoom.name;
            guestPayoutText.text = room.currentGuestInRoom.payout.ToString();

            roomLimitText.text = room.GetRoomLimit().ToString();
            totalMoneyText.text = room.storedMoney.ToString();

            if (currentGuestRoom.isRoomDisabled)
            {
                DisableRoom();
                if (!currentGuestRoom.isCleaning)
                {
                    cleanRoomButton.gameObject.SetActive(true);
                    cleaningSlider.gameObject.SetActive(false);
                }
                else
                {
                    cleanRoomButton.gameObject.SetActive(false);
                    cleaningSlider.gameObject.SetActive(true);
                }
            }
        }

        private void Update()
        {
            if (currentGuestRoom != null || guestRoomPanel.activeInHierarchy)
            {
                guestCurrentStayTimeText.text = Mathf.RoundToInt(currentGuestRoom.GetCurrentStayTime()).ToString();
                guestStayTimeSlider.value = currentGuestRoom.GetNormalizedStayTime();
            }

            janitorAmountText.text = Room_Janitor.GetAvailableJanitor().ToString();

            if (cleanRoomButton.gameObject.activeInHierarchy)
            {
                if (Room_Janitor.GetAvailableJanitor() <= 0 || !currentGuestRoom.isRoomDisabled)
                {
                    cleanRoomButton.interactable = false;
                }
                else
                {
                    cleanRoomButton.interactable = true;
                }
            }
        }

        public void ClosePanel()
        {
            panel.SetActive(false);
            currentGuestRoom = null;
        }

        public void CollectMoney()
        {
            gameManager.MoneyManager.AddMoney(currentGuestRoom.storedMoney);
            currentGuestRoom.storedMoney = 0;
            totalMoneyText.text = "0";
        }

        public void DestroyRoomButton()
        {
            currentGuestRoom.DestroyRoom();
            currentGuestRoom = null;
            ClosePanel();
        }

        public void DisableRoom()
        {
            guestRoomPanel.SetActive(false);
        }

        public void CleanRoom()
        {
            if (currentGuestRoom)
            {
                currentGuestRoom.CleanRoom();
            }

            cleanRoomButton.gameObject.SetActive(false);
            cleaningSlider.gameObject.SetActive(true);
        }

        public void FinishCleaning()
        {
            cleanRoomButton.gameObject.SetActive(true);
            cleaningSlider.gameObject.SetActive(false);
            guestRoomPanel.SetActive(true);
        }

        public void SetCleaningSlider(float value)
        {
            cleaningSlider.value = value;
        }
    }
}
