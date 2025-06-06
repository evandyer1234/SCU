using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyPlaceholder : MonoBehaviour
    {
        private Vector3 lastKnownKidneyPosition;
        public bool isLeftKidneyPlaceholder;
        public bool isEmpty = false;

        public void SetLastKnownKidneyPosition(Vector3 kidneyPosition)
        {
            lastKnownKidneyPosition = kidneyPosition;
        }

        public Vector3 GetLastKnownKidneyPosition()
        {
            return lastKnownKidneyPosition;
        }
    }
}