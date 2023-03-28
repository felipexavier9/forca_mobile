using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Collections;

namespace DTT.Hangman.Demo
{
    /// <summary>
    /// A behaviour managing a hint button.
    /// </summary>
    public class HintButton : MonoBehaviour
    {
        /// <summary>
        /// The game service.
        /// </summary>
        [SerializeField]
        private HangmanService _service;

        /// <summary>
        /// The hint renderer.
        /// </summary>
        [SerializeField]
        private Text _hintRenderer;

        [SerializeField]
        private Animator _hintAnimator;
        
        /// <summary>
        /// Whether the hint should be random. If set to false, the first hint will always be used.
        /// </summary>
        [SerializeField]
        private bool _randomHint = true;
        
        /// <summary>
        /// The button to be clicked.
        /// </summary>
        private Button _button;

        /// <summary>
        /// Starts listening to the button.
        /// </summary>
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Called when the button is clicked, it will get the
        /// the phrase descriptions and display one as hint.
        /// </summary>
        private void OnClick()
        {
            
            ShowHint();
        }

        public void ShowHint(){
            string[] descriptions = _service.CurrentPhrase.descriptions;
            if (descriptions == null || descriptions.Length == 0)
            {
                _hintRenderer.text = "No hint available";   
                //_hintAnimator.enabled = true;
                //_hintAnimator.Play("FadeOut");
            }
            else
            {
                string hint = _randomHint ? descriptions[Random.Range(0, descriptions.Length)] : descriptions[0];
                _hintRenderer.text = $"Hint: {hint}";
                //_hintAnimator.enabled = true;
                //_hintAnimator.SetTrigger("ShowHint");
            }
        }
    }
}
