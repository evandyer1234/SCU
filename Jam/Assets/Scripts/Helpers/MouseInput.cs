using UnityEngine;

namespace Helpers
{
    /**
     * Filters InputActions from the new input system for pressdown (value > 0) / release (value == 0) events
     */
    public class MouseInput
    {

        public static Vector2 ScreenPosition(SCUInputAction action)
        {
            return action.UI.Point.ReadValue<Vector2>();
        }

        public static Vector2 WorldPosition(SCUInputAction action)
        {
            Vector2 screenPos = ScreenPosition(action);
            return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        }
        
        public static bool LeftClicked(SCUInputAction action)
        {
            return action.UI.Click.WasPressedThisFrame();
        }
        
        public static bool LeftReleased(SCUInputAction action)
        {
            return action.UI.Click.WasReleasedThisFrame();
        }

        public static bool IsLeftPressed(SCUInputAction action)
        {
            return action.UI.Click.IsPressed();
        }
        
        public static bool RightClicked(SCUInputAction action)
        {
            return action.UI.RightClick.WasPressedThisFrame();
        }
        
        public static bool RightReleased(SCUInputAction action)
        {
            return action.UI.RightClick.WasReleasedThisFrame();
        }
        
        public static bool ScrollForward(SCUInputAction action)
        {
            return action.UI.ScrollWheel.ReadValue<Vector2>().y > 0f;
        }
        
        public static bool ScrollBackward(SCUInputAction action)
        {
            return action.UI.ScrollWheel.ReadValue<Vector2>().y < 0f;
        }
    }
}