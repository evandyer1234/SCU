using System.Collections.Generic;
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
        [SerializeField] private GameObject outputTray;
        
        private SCUInputAction _scuInputAction;
        private bool allCorruptedOnTrayCondition = false;
        private bool allOrgansConnectedAreHealthyCondition = false;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            ingredient = IngredientConstants.INGREDIENT_ID_DRAGONS_BLOOD;
        }

        public override void Start()
        {
            base.Start();
            AssignRandomCorruptedOrgans();
        }

        private void FixedUpdate()
        {
            if (allCorruptedOnTrayCondition && allOrgansConnectedAreHealthyCondition)
            {
                OnSuccess();
                return;
            }
            
            if (!liver.IsCorrupted()
                && !stomach.IsCorrupted()
                && !leftKidney.IsCorrupted()
                && !rightKidney.IsCorrupted())
            {
                allOrgansConnectedAreHealthyCondition = true;
            }
            
            var outputElements = Physics2D.OverlapCircleAll(outputTray.transform.position, 2.5f);
            if (outputElements.Length <= 0) return;
            if (allCorruptedOnTrayCondition) return;
            
            var outputSet = new HashSet<string>();
            foreach (var outputElement in outputElements)
            {
                outputSet.Add(IsCorruptedLiverOnTray(outputElement));
                outputSet.Add(IsCorruptedStomachOnTray(outputElement));
                outputSet.Add(IsCorruptedKidneyOnTray(outputElement));
            }

            // null, (liver/stomach), (leftKidney/rightKidney)
            if (outputSet.Count == 3)
            {
                allCorruptedOnTrayCondition = true;
            }
        }
        
        public void AssignNewStomach(StomachMovable stomachMovable)
        {
            this.stomach = stomachMovable;
        }
        
        public void AssignNewLiver(LiverMovable liverMovable)
        {
            this.liver = liverMovable;
        }
        
        public void AssignNewKidney(KidneyMovable kidneyMovable)
        {
            if (kidneyMovable.isLefKidney)
            {
                this.leftKidney = kidneyMovable;
            }
            else
            {
                this.rightKidney = kidneyMovable;
            }
        }

        public KidneyMovable GetLeftKidneyRef()
        {
            return this.leftKidney;
        }
        
        public KidneyMovable GetRightKidneyRef()
        {
            return this.rightKidney;
        }

        public LiverMovable GetLiverRef()
        {
            return this.liver;
        }

        public StomachMovable GetStomachRef()
        {
            return this.stomach;
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
        
        private string IsCorruptedLiverOnTray(Collider2D outputElement)
        {
            if (outputElement.gameObject.GetComponent<LiverMovable>() != null)
            {
                var liverComp = outputElement.gameObject.GetComponent<LiverMovable>();
                if (liverComp.IsCorrupted())
                {
                    return liverComp.gameObject.name;
                }
            }

            return null;
        }
        
        private string IsCorruptedStomachOnTray(Collider2D outputElement)
        {
            if (outputElement.gameObject.GetComponent<StomachMovable>() != null)
            {
                var stomachComp = outputElement.gameObject.GetComponent<StomachMovable>();
                if (stomachComp.IsCorrupted())
                {
                    return stomachComp.gameObject.name;
                }
            }

            return null;
        }
        
        private string IsCorruptedKidneyOnTray(Collider2D outputElement)
        {
            if (outputElement.gameObject.GetComponent<KidneyMovable>() != null)
            {
                var kidneyComp = outputElement.gameObject.GetComponent<KidneyMovable>();
                if (kidneyComp.IsCorrupted())
                {
                    return kidneyComp.gameObject.name;
                }
            }

            return null;
        }
    }
}