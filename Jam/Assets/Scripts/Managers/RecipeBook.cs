using Helpers;
using UnityEngine;

namespace Managers
{
    public class RecipeBook : MonoBehaviour
    {

        [SerializeField] private TMPro.TMP_Text _openCloseButtonText;
        [SerializeField] private GameObject _recipe01;
        [SerializeField] private GameObject _recipe02;
        [SerializeField] private GameObject _recipe03;
        [SerializeField] private GameObject _buttonNextPage;
        [SerializeField] private GameObject _buttonPreviousPage;
        [SerializeField] private TMPro.TMP_Text _patientInfoText;
        [SerializeField] private TMPro.TMP_Text _ingredientHintsText;
        
        private Vector3 _closedPosition;
        private Vector3 _openPosition;
        private bool _recipeBookOpen = false;
        private int _pageIndex;
        
        private SubjectManager _subjectManager;

        private void Awake()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
            _closedPosition = transform.position;
            var clp = _closedPosition;
            _openPosition = new Vector3(clp.x, clp.y + 10f, clp.z);
            _openCloseButtonText.text = "Open Recipe Book";
            _pageIndex = 0;
            SetRecipeActiveByIndex(_pageIndex);
            SetPatientInfo();
        }

        public void ToggleRecipeBook()
        {
            _recipeBookOpen = !_recipeBookOpen;
            if (_recipeBookOpen)
            {
                transform.position = _openPosition;
                _openCloseButtonText.text = "Close Recipe Book";
            }
            else
            {
                transform.position = _closedPosition;
                _openCloseButtonText.text = "Open Recipe Book";
            }
        }

        public void NextPage()
        {
            _pageIndex++;
            if (_pageIndex >= 2) _pageIndex = 2;
            SetRecipeActiveByIndex(_pageIndex);
        }

        public void PreviousPage()
        {
            _pageIndex--;
            if (_pageIndex <= 0) _pageIndex = 0;
            SetRecipeActiveByIndex(_pageIndex);
        }

        private void DisableAllRecipes()
        {
            _recipe01.SetActive(false);
            _recipe02.SetActive(false);
            _recipe03.SetActive(false);
        }

        private void SetRecipeActiveByIndex(int index)
        {
            DisableAllRecipes();
            _buttonPreviousPage.SetActive(true);
            _buttonNextPage.SetActive(true);
            switch (index)
            {
                case 0:
                    _recipe01.SetActive(true);
                    _buttonPreviousPage.SetActive(false);
                    break;
                case 1:
                    _recipe02.SetActive(true);
                    break;
                case 2:
                    _recipe03.SetActive(true);
                    _buttonNextPage.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Page Index out of bounds for given Recipes");
                    break;
            }
        }
        
        private void SetPatientInfo()
        {
            var currentSubject = _subjectManager.currentSubject;
            if (currentSubject == null) return;
            
            _patientInfoText.text = "Patient Info: " + currentSubject.name;
            var ingredientHints = _subjectManager.GetIngredientHints();
            string ingredientsToDisplay = "Ingredients:\n";
            foreach (var ingName in ingredientHints)
            {
                ingredientsToDisplay += "\n* " + ingName;
            }
            _ingredientHintsText.text = ingredientsToDisplay;
        }
    }
}
