using System.Collections.Generic;
using DefaultNamespace;
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
        
        private PauseMenuManager _pauseMenuManager;
        private HelpInstructions _helpInstructions;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }

        public override void Start()
        {
            base.Start();
            AssignRandomCorruptedOrgans();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            _helpInstructions = GameObject.FindObjectOfType<HelpInstructions>();
        }

        private void FixedUpdate()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (_helpInstructions.isHelpOpen()) return;
            
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
            if (_pauseMenuManager.isGamePaused()) return;
            if (_helpInstructions.isHelpOpen()) return;
            
            this.stomach = stomachMovable;
        }
        
        public void AssignNewLiver(LiverMovable liverMovable)
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (_helpInstructions.isHelpOpen()) return;
            
            this.liver = liverMovable;
        }
        
        public void AssignNewKidney(KidneyMovable kidneyMovable)
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (_helpInstructions.isHelpOpen()) return;
            
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