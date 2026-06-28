using UnityEngine;

namespace prototype3
{
    public enum DoorNumber
    {
        Door1,
        Door2,
        Door3,
        Door4,
        Door5
    }

    public class PressurePlate : MonoBehaviour
    {
        public delegate void ToggleEvent(DoorNumber doorId);

        public static event ToggleEvent Open;
        public static event ToggleEvent Close;
        
        public DoorNumber doorID;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Weight"))
            {
                Debug.Log("Pressure Plate collided with " + other.name);
                
                Open?.Invoke(doorID);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
                Close?.Invoke(doorID);
        }
    }
}