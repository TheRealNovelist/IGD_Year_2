using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Obsolete
{
    [System.Serializable]
    public struct GuestroomRanges
    {
        public Vector2 stayTimeRange;
        public Vector2 payoutRange;
    }

    public class Room_Guest : Room_Framework
    {
        private static readonly string[] guestNames = new[] { "Harry", "David", "Judah", "James", "Lenny", "Arthur", "Akira", "Manny" };

        public Guest currentGuestInRoom;

        public bool isRoomDisabled;
        public bool isCleaning;

        [Header("Economy Settings")]
        [SerializeField] private int maxRoomLimit = 20;
        public int storedMoney = 0;

        public GuestroomRanges guestroomRanges;

        public GuestroomUI guestroomUI;

        //UI purposes
        private float currentStayTime;
        private int currentRoomLimit;

        private void Start()
        {
            currentRoomLimit = maxRoomLimit;

            guestroomUI = FindObjectOfType<GuestroomUI>();

            GenerateGuest();
        }

        private void OnMouseDown()
        {
            guestroomUI.OpenPanel(this);
        }

        private void GenerateGuest()
        {
            float scale = Random.Range(0f, 1f);
            currentGuestInRoom.name = guestNames[Random.Range(0, guestNames.Length - 1)];
            currentGuestInRoom.stayTime = Mathf.Lerp(guestroomRanges.stayTimeRange.x, guestroomRanges.stayTimeRange.y, scale);
            currentGuestInRoom.payout = Mathf.RoundToInt(Mathf.Lerp(guestroomRanges.payoutRange.x, guestroomRanges.payoutRange.y, scale));

            currentStayTime = currentGuestInRoom.stayTime;

            StartGuestTimer();
        }

        private void DisableRoomOnLimit()
        {
            isRoomDisabled = true;
            if (guestroomUI.currentGuestRoom == this)
            {
                guestroomUI.DisableRoom();
            }
        }

        public int GetRoomLimit()
        {
            return currentRoomLimit;
        }

        public float GetCurrentStayTime()
        {
            return currentStayTime;
        }

        public float GetNormalizedStayTime()
        {
            return currentStayTime / currentGuestInRoom.stayTime;
        }

        #region Guest Timer
        public void StartGuestTimer()
        {
            StartCoroutine(GuestTimerRoutine());
        }
        IEnumerator GuestTimerRoutine()
        {
            while (currentStayTime > 0)
            {
                currentStayTime -= Time.deltaTime;
                yield return null;
            }
            OnTimerEnd();
        }

        void OnTimerEnd()
        {
            storedMoney += currentGuestInRoom.payout;
            currentRoomLimit--;

            if (guestroomUI.currentGuestRoom == this)
            {
                guestroomUI.PopulatePanel(this);
            }

            if (currentRoomLimit > 0)
            {
                GenerateGuest();
            }
            else
            {
                DisableRoomOnLimit();
            }
        }

        #endregion

        public void CleanRoom()
        {
            //Deploy a janitor
            Room_Janitor.DeployJanitor();
            StartCoroutine(Cleaning(10));
        }

        public IEnumerator Cleaning(float duration)
        {
            float currentTime = duration;
            isCleaning = true;

            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;

                if (guestroomUI.currentGuestRoom == this)
                {
                    guestroomUI.SetCleaningSlider(currentTime / duration);
                }

                yield return null;
            }

            isCleaning = false;
            FinishCleaning();
        }

        public void FinishCleaning()
        {
            isRoomDisabled = false;
            currentRoomLimit = maxRoomLimit;

            //Resume guest generation loop
            GenerateGuest();

            //Recover the janitor
            Room_Janitor.RetrieveJanitor();

            if (guestroomUI.currentGuestRoom == this)
            {
                guestroomUI.FinishCleaning();
                guestroomUI.PopulatePanel(this);
            }
        }
    }
}
