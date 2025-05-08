namespace Helpers
{
    public class KeyboardInput
    {
        public static bool EscapePressed(SCUInputAction action)
        {
            return action.UI.Escape.WasPressedThisFrame();
        }
        
        public static bool SpacebarPressed(SCUInputAction action)
        {
            return action.UI.Space.WasPressedThisFrame();
        }
    }
}