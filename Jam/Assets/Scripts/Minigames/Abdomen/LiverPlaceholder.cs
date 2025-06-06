using UnityEngine;

namespace Minigames.Abdomen
{
    public class LiverPlaceholder : MonoBehaviour
    {
        private Vector3 lastKnownLiverPosition;
        private Vector3 lastKnownLiverRootPosition;
        public bool isEmpty = false;

        public void SetLastKnownLiverPosition(Vector3 position)
        {
            this.lastKnownLiverPosition = position;
        }

        public void SetLastKnownLiverRootPosition(Vector3 position)
        {
            this.lastKnownLiverRootPosition = position;
        }

        public Vector3 GetLastKnownLiverPosition()
        {
            return this.lastKnownLiverPosition;
        }

        public Vector3 GetLastKnownLiverRootPosition()
        {
            return this.lastKnownLiverRootPosition;
        }
    }
}