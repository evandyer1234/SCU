using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class MG_Abdomen : MiniGameBase
    {
        [SerializeField] private StomachMovable stomach;
        [SerializeField] private LiverMovable liver;
        [SerializeField] private KidneyMovable leftKidney;
        [SerializeField] private KidneyMovable rightKidney;
        
        // offsets on drag
        private Vector3 offsetLeftKidney;
        private Vector3 offsetRightKidney;
        
        private bool followMouse;
        private SCUInputAction _scuInputAction;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            AssignRandomCorruptedOrgans();
        }

        private void Update()
        {
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);
                KeepSpriteRelativeToMouse(leftKidney.gameObject, mouseWorldPos, offsetLeftKidney);
                KeepSpriteRelativeToMouse(rightKidney.gameObject, mouseWorldPos, offsetRightKidney);
            }
        }

        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
        }

        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetLeftKidney = (mousepos - leftKidney.transform.position);
            offsetRightKidney = (mousepos - rightKidney.transform.position);
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }

        private void AssignRandomCorruptedOrgans()
        {
            var corruptLiver = Random.Range(0, 1f) > .5f;
            var corruptLeftKidney = Random.Range(0, 1f) > .5f;

            if (corruptLiver)
            {
                liver.SetCorrupted();
            }
            else
            {
                stomach.SetCorrupted();
            }

            if (corruptLeftKidney)
            {
                leftKidney.SetCorrupted();
            }
            else
            {
                rightKidney.SetCorrupted();
            }
        }
    }
}