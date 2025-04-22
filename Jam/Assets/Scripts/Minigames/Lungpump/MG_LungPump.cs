using Helpers;
using UnityEngine;

namespace Minigames.Lungpump
{
    public class MG_LungPump : MiniGameBase
    {
        [SerializeField, Tooltip("A reference to the barometer needle. Has to point straight up initially")] 
        private GameObject barometerNeedle;

        [SerializeField, Tooltip("A reference to the left pump")]
        private GameObject lungPump;
        
        private static float NEEDLE_MIN_ANGLE = 145;
        private static float NEEDLE_MAX_ANGLE = -145;
        
        private void Start()
        {
            ResetBarometerNeedleToMinimum();
            InvokeRepeating(nameof(EvaluatePressure), 3f, 1f);
        }

        private void EvaluatePressure()
        {
            bool isWithinRange = (barometerNeedle.transform.rotation.eulerAngles.z is >= 0 and <= 15)
                                 || (barometerNeedle.transform.rotation.eulerAngles.z is <= 360 and >= 345);
            Debug.Log(isWithinRange);
        }
        
        private void OnMouseOver()
        {
            if(MouseInput.LeftClick()) HandleLeftClick();
        }

        private void HandleLeftClick()
        {
            float variatingPumpIncrement = UnityEngine.Random.Range(12f, 16f);
            barometerNeedle.transform.Rotate(0, 0, -variatingPumpIncrement);
        }
        
        private void FixedUpdate()
        {
            float naturalPressureDrop = UnityEngine.Random.Range(0.4f, 2.1f);
            
            barometerNeedle.transform.Rotate(0, 0, naturalPressureDrop);
            if (IsBarometerBelowLowest())
            {
                ResetBarometerNeedleToMinimum();
            }

            if (IsBarometerAboveHighest())
            {
                ResetBarometerNeedleToMaximum();
            }
        }

        private void ResetBarometerNeedleToMinimum()
        {
            barometerNeedle.transform.rotation = Quaternion.Euler(0, 0, NEEDLE_MIN_ANGLE);
        }
        
        private void ResetBarometerNeedleToMaximum()
        {
            barometerNeedle.transform.rotation = Quaternion.Euler(0, 0, NEEDLE_MAX_ANGLE);
        }
        
        private bool IsBarometerBelowLowest()
        {
            return barometerNeedle.transform.rotation.eulerAngles.z is > 145 and < 180;
        }
        
        private bool IsBarometerAboveHighest()
        {
            return barometerNeedle.transform.rotation.eulerAngles.z is < 218 and > 180;
        }
    }
}