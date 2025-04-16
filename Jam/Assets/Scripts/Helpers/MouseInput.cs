using UnityEngine;

namespace Helpers
{
    /**
     * Make sure to wrap all Mouse Interaction through here.
     * This will help us to convert to Unitys new Input System in a central place.
     */
    public class MouseInput
    {
        public static bool LeftClick()
        {
            return Input.GetMouseButtonDown(0);
        }

        public static bool ScrollForward()
        {
            return Input.GetAxis("Mouse ScrollWheel") > 0f;
        }
        
        public static bool ScrollBackward()
        {
            return Input.GetAxis("Mouse ScrollWheel") < 0f ;
        }
    }
}