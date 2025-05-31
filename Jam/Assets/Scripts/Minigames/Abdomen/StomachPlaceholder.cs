using UnityEngine;

namespace Minigames.Abdomen
{
    public class StomachPlaceholder : MonoBehaviour
    {
        private Vector3 lastKnownStomachPosition;
        private Vector3 lastKnownStomachRootPosition;
        public bool isEmpty = false;

        public void SetLastKnownStomachPosition(Vector3 stomachPosition)
        {
            this.lastKnownStomachPosition = stomachPosition;
        }

        public Vector3 GetLastKnownStomachPosition()
        {
            return lastKnownStomachPosition;
        }

        public void SetLastKnownStomachRootPosition(Vector3 stomachRootPosition)
        {
            this.lastKnownStomachRootPosition = stomachRootPosition;
        }
        
        public Vector3 GetLastKnownStomachRootPosition()
        {
            return lastKnownStomachRootPosition;
        }
    }
}