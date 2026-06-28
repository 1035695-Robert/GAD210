using System;
using System.Linq;
using UnityEngine;

namespace prototype3
{
    public class Door : MonoBehaviour
    {
        public DoorNumber[] doorID;
        [SerializeField] private bool[] doorsOpen;

        private void OnEnable()
        {
            PressurePlate.Open += OpenDoor;
            PressurePlate.Close += CloseDoor;
            doorsOpen = new bool[doorID.Length];
        }

        private void OpenDoor(DoorNumber id)
        {
            for (int i = 0; i < doorID.Length; i++)
            {
                if (doorID[i] == id)
                {
                    doorsOpen[i] = true;
                    if (doorsOpen.All(x => x))
                       transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        private void CloseDoor(DoorNumber id)
        {
            for (int i = 0; i < doorID.Length; i++)
            {
                if (doorID[i] == id)
                {
                    doorsOpen[i] = false;
                    transform.GetChild(0).gameObject.SetActive(true);

                }
            }
        }
    }
}