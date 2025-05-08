using UnityEngine;

namespace UI
{
    public class InteractionHint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer interactionHint;
        [SerializeField] private float animationLength;
        [SerializeField] private int totalAnimationTimes = 3;

        private bool startAnimation = false;
        private int count = 0;
        private int animatedTimes = 0;
        
        private void Awake()
        {
            interactionHint.enabled = false;
        }

        private void FixedUpdate()
        {
            if (!startAnimation) return;
            if (animatedTimes >= totalAnimationTimes) return;
            
            ExecuteAnimationCalc();
        }

        private void ExecuteAnimationCalc()
        {
            if (count > animationLength) return;
            count++;

            var c = interactionHint.color;
            var alpha = c.a;
            if (count <= animationLength)
            {
                if (count <= animationLength / 2)
                {
                    alpha = count / (animationLength / 2);
                }
                else
                {
                    alpha = 1 - ((count - animationLength / 2) / (animationLength / 2));
                }
            }
            else
            {
                alpha = 0;
                animatedTimes += 1;
                if (animatedTimes >= totalAnimationTimes)
                {
                    return;
                }
                count = 0;
            }
            
            interactionHint.color = new Color(c.r, c.g, c.b, alpha);
        }

        public void TriggerAnimation()
        {
            animatedTimes = 0;
            count = 0;
            startAnimation = true;
            var c = interactionHint.color;
            interactionHint.color = new Color(c.r, c.g, c.b, 0);
            interactionHint.enabled = true;
        }
    }
}