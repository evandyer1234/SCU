using UI;
using UnityEngine;

namespace Minigames.HeartString
{
    public class HeartStringPattern : MonoBehaviour
    {
        [SerializeField, Tooltip("The hints for which to display clicking hints")]
        private InteractionHint[] _veinOutlineHints;

        void Start()
        {
            Invoke(nameof(AnimateVeinClickAreas), 2f);
        }
        
        private void AnimateVeinClickAreas()
        {
            foreach (var _veinHint in _veinOutlineHints)
            {
                _veinHint.TriggerAnimation();
            }
        }
    }
}