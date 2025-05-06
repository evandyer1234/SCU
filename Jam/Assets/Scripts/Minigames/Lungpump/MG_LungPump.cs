using Helpers;
using UI;
using UnityEngine;

namespace Minigames.Lungpump
{
    public class MG_LungPump : MiniGameBase
    {
        [SerializeField, Tooltip("A reference to the barometer needle. Has to point straight up initially")] 
        private GameObject barometerNeedle;

        [SerializeField, Tooltip("A reference to the lung pumps")]
        private GameObject lungPump;
        
        [SerializeField, Tooltip("A reference to the lung graphics")]
        private GameObject lungs;
        
        [SerializeField, Tooltip("Amount of times to evaluate success. Evaluated each evaluation tick.")]
        private int ticksToSuccess;

        [SerializeField, Tooltip("Left Pump Click hint.")]
        private InteractionHint _hintLeft;
        
        [SerializeField, Tooltip("Right Pump Click hint.")]
        private InteractionHint _hintRight;
        
        [SerializeField, Tooltip("Barometer Target Area hint.")]
        private InteractionHint _barometerTargetAreaHint;
        
        // barometer needle
        private const float NEEDLE_MIN_ANGLE = 145;
        private const float NEEDLE_MAX_ANGLE = 218;
        private const float NEEDLE_POINT_DOWN = 180;
        private const float NEEDLE_POINT_UP_360 = 360;
        private const float NEEDLE_POINT_UP_0 = 0;

        // lung sprites and addressable names
        private static string LUNGS_SMALL = "lungs_small";
        private static string LUNGS_MEDIUM = "lungs_medium";
        private static string LUNGS_BIG = "lungs_big";
        private static Sprite lungsSmallSprite;
        private static Sprite lungsMediumSprite;
        private static Sprite lungsBigSprite;
        
        // alternating on click collider
        private static bool colliderIsLeft = true;
        private static CircleCollider2D _onClickCollider;
        private static Vector2 _collOffsetLeft = new Vector2(3f, 7f);
        private static Vector2 _collOffsetRight = new Vector2(12.5f, 7f);

        private SCUInputAction _scuInputAction;

        private bool _usedOnce = false;
        
        private void Awake()
        {
            lungsSmallSprite = FileLoader.GetSpriteByName(LUNGS_SMALL);
            lungsMediumSprite = FileLoader.GetSpriteByName(LUNGS_MEDIUM);
            lungsBigSprite = FileLoader.GetSpriteByName(LUNGS_BIG);
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void Start()
        {
            base.Start();
            ingredient = NamingConstants.INGREDIENT_ID_PEARL_ASH;
            
            _onClickCollider = GetComponent<CircleCollider2D>();
            _onClickCollider.offset = _collOffsetLeft;
            colliderIsLeft = true;
            ResetBarometerNeedleToMinimum();
            InvokeRepeating(nameof(EvaluatePressure), 3f, 1f);
            Invoke(nameof(AnimateClickHints), 2f);
        }
        
        private void EvaluatePressure()
        {
            if (IsPressureWithinSuccessRange())
            {
                SetLungSprite(lungsBigSprite);
                ticksToSuccess--;
            } else if (IsPressureWithinMediumRange())
            {
                SetLungSprite(lungsMediumSprite);
            }
            else
            {
                SetLungSprite(lungsSmallSprite);
            }

            if (ticksToSuccess <= 0)
            {
                CancelInvoke(nameof(EvaluatePressure));
                OnSuccess();
            }
        }

        private bool IsPressureWithinSuccessRange()
        {
            return (barometerNeedle.transform.rotation.eulerAngles.z is >= NEEDLE_POINT_UP_0 and <= 36)
                || (barometerNeedle.transform.rotation.eulerAngles.z is <= NEEDLE_POINT_UP_360 and >= 327);
        }

        private bool IsPressureWithinMediumRange()
        {
            return (barometerNeedle.transform.rotation.eulerAngles.z is > 20 and <= 80)
                   || (barometerNeedle.transform.rotation.eulerAngles.z is < 340 and >= 280);
        }
        
        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                HandleLeftClick();
            }
        }
        
        private void HandleLeftClick()
        {
            if (!_usedOnce)
            {
                Invoke(nameof(AnimateBarometerTargetHint), 3f);
            }
            
            _usedOnce = true;
            if (colliderIsLeft)
            {
                lungPump.GetComponent<SpriteRenderer>().flipX = true;
                _onClickCollider.offset = _collOffsetRight;
            }
            else
            {
                lungPump.GetComponent<SpriteRenderer>().flipX = false;
                _onClickCollider.offset = _collOffsetLeft;
            }

            colliderIsLeft = !colliderIsLeft;
            float variatingPumpIncrement = Random.Range(15f, 22f);
            barometerNeedle.transform.Rotate(0, 0, -variatingPumpIncrement);
        }
        
        private void FixedUpdate()
        {
            float naturalPressureDrop = Random.Range(0.2f, 1f);
            if (IsPressureWithinSuccessRange())
            {
                // make it more difficult to keep within success range
                naturalPressureDrop = Random.Range(0.5f, 1.5f);
            }
            
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
            return barometerNeedle.transform.rotation.eulerAngles.z is > NEEDLE_MIN_ANGLE and < NEEDLE_POINT_DOWN;
        }
        
        private bool IsBarometerAboveHighest()
        {
            return barometerNeedle.transform.rotation.eulerAngles.z is < NEEDLE_MAX_ANGLE and > NEEDLE_POINT_DOWN;
        }

        private void SetLungSprite(Sprite sprite)
        {
            lungs.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
        private void AnimateClickHints()
        {
            if (_usedOnce) return;
            
            _hintLeft.TriggerAnimation();
            Invoke(nameof(AnimateRightClickHint), 0.6f);
        }

        private void AnimateRightClickHint()
        {
            _hintRight.TriggerAnimation();
        }

        private void AnimateBarometerTargetHint()
        {
            _barometerTargetAreaHint.TriggerAnimation();
        }

    }
}