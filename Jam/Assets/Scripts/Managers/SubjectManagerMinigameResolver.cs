using System.Collections.Generic;
using Helpers;
using Subjects;
using UnityEngine;

namespace Managers
{
    public class SubjectManagerMinigameResolver
    {
        public static GameObject GetMinigameByMinigameId(string minigameId, List<MiniGameBase> minigames)
        {
            foreach (var minigame in minigames)
            {
                if (minigame.CompareTag(minigameId)) return minigame.gameObject;
            }

            Debug.LogWarning($"Failed to load matching minigame from MinigameManager by tag {minigameId}");
            return null;
        }
        
        public static GameObject GetMagnifyingGlassLensForMinigameId(string minigameId, MagnifyingGlass magnifyingGlass)
        {
            switch (minigameId)
            {
                case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                    return magnifyingGlass.leftLensReference;
                case NamingConstants.TAG_MINIGAME_LUNGPUMP:
                    return magnifyingGlass.leftLensReference;
                case NamingConstants.TAG_MINIGAME_DRAIN:
                    return magnifyingGlass.middleLensReference;
            }
        
            Debug.LogWarning($"Failed to match Magnifying Glass Lens for minigameId {minigameId}");
            return null;
        }
        
        public static GameObject GetSubjectScanLayerFromMinigameId(string minigameId, PatientPage patientPage)
        {
            switch (minigameId)
            {
                case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                    return patientPage.organLayer;
                case NamingConstants.TAG_MINIGAME_LUNGPUMP:
                    return patientPage.organLayer;
                case NamingConstants.TAG_MINIGAME_DRAIN:
                    return patientPage.underClothLayer;
            }

            Debug.LogWarning($"Failed to match Patient Scan Layer for minigameId {minigameId}");
            return null;
        }
    }
}