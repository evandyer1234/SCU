using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Subjects
{
    public class CorruptionMarkPositions
    {
        private static List<Vector2> lungPumpAdultPositions = new List<Vector2>()
        {
            new (5, 12.6f),
            new (2.8f, 12.6f),
            new (3.2f, 13.4f),
            new (4.6f, 13.4f),
        };
        
        private static List<Vector2> lungPumpChildPositions = new List<Vector2>()
        {
            new (3.2f, 13.4f),
            new (2.8f, 11.9f),
            new (4.6f, 13f),
            new (4.8f, 11.9f),
        };
        
        private static List<Vector2> heartStringAdultPositions = new List<Vector2>()
        {
            new (3.9f, 13.5f),
            new (3.5f, 14f),
            new (4.2f, 14f),
        };
        
        private static List<Vector2> heartStringChildPositions = new List<Vector2>()
        {
            new (3.5f, 13.3f),
            new (4f, 13.1f),
            new (3.8f, 12.4f),
        };
        
        private static List<Vector2> drainAdultPositions = new List<Vector2>()
        {
            new (6.5f, 9.5f),
            new (6.5f, 11f),
            new (6f, 12f),
            new (1.5f, 12f),
            new (1.5f, 10.5f),
            new (1.5f, 9.5f),
        };
        
        private static List<Vector2> drainChildPositions = new List<Vector2>()
        {
            new (6f, 9.5f),
            new (5.5f, 11f),
            new (5.5f, 12f),
            new (2f, 12f),
            new (1.5f, 10.5f),
            new (1.5f, 9f),
        };

        private static List<Vector2> abdomenAdultPositions = new List<Vector2>()
        {
            new(3.8f, 12.2f),
            new(3.4f, 11.7f),
            new(4.4f, 11.7f),
        };
        
        private static List<Vector2> abdomenChildPositions = new List<Vector2>()
        {
            new(3.8f, 11.6f),
            new(3.4f, 11.1f),
            new(4.4f, 11.1f),
        };
        
        public static Vector2 GetPositionByMinigameId(string minigameId, bool isAdult)
        {
            switch (minigameId)
            {
                case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                    return GetRandomPosition(isAdult ? heartStringAdultPositions : heartStringChildPositions);
                case NamingConstants.TAG_MINIGAME_LUNGPUMP:
                    return GetRandomPosition(isAdult ? lungPumpAdultPositions : lungPumpChildPositions);
                case NamingConstants.TAG_MINIGAME_DRAIN:
                    return GetRandomPosition(isAdult ? drainAdultPositions : drainChildPositions);
                case NamingConstants.TAG_MINIGAME_ABDOMEN:
                    return GetRandomPosition(isAdult ? abdomenAdultPositions : abdomenChildPositions);
            }
        
            Debug.LogWarning($"Failed to match Corruption Mark Position for minigameId {minigameId}");
            return Vector2.zero;
        }

        private static Vector2 GetRandomPosition(List<Vector2> positions)
        {
            var randomIndex = Random.Range(0, positions.Count);
            return positions[randomIndex];
        }
    }
}
