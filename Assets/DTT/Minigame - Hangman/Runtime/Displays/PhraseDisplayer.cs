using System.Collections.Generic;
using System;
using UnityEngine;

namespace DTT.Hangman
{
    /// <summary>
    /// A representation of a behaviour that displays a phrase.
    /// </summary>
    public class PhraseDisplayer : MonoBehaviour, IGameplaySectionController
    {
        /// <summary>
        /// The prefab of the letter game object.
        /// </summary>
        [SerializeField]
        private GameObject _letterPrefab;

        /// <summary>
        /// The prefab of the letter game object.
        /// </summary>
        public GameObject LetterPrefab => _letterPrefab;

        /// <summary>
        /// The letter display instances in the scene.
        /// </summary>
        public LetterDisplayer[] LetterDisplayers { get; private set; }

        /// <summary>
        /// Returns the transform of the parent transform for the letters in the scene.
        /// </summary>
        /// <returns>The parent transform.</returns>
        public virtual Transform GetLetterParentInScene() => transform;

        /// <summary>
        /// Generates the phrase display.
        /// </summary>
        /// <param name="service">The game service.</param>
        /// <param name="list">The list the phrase is chosen from.</param>
        /// <param name="phrase">The phrase for the game.</param>
        public virtual void Generate(HangmanService service, PhraseList list, Phrase phrase)
        {
            LetterDisplayers = new LetterDisplayer[phrase.Length];

            Transform parent = GetLetterParentInScene();
            for (int i = 0; i < LetterDisplayers.Length; i++)
            {
                GameObject instance = Instantiate(_letterPrefab, parent);
                LetterDisplayer displayer = instance.GetComponent<LetterDisplayer>();
                
                displayer.Setup(phrase[i]);
                
                // Set the letter value if we are showing vowels and the letter is a vowel.
                if(phrase.Exposes(phrase[i]))
                    displayer.SetLetterValue();

                LetterDisplayers[i] = displayer;
            }
        }

        /// <summary>
        /// Clears the display.
        /// </summary>
        public virtual void Clear()
        {
            for(int i = 0; i < LetterDisplayers.Length; i++)
                LetterDisplayers[i].Clear();
        }

        /// <summary>
        /// Returns whether the full phrase is being displayed.
        /// </summary>
        /// <returns>Whether the full phrase is being displayed.</returns>
        public virtual bool IsFullPhraseDisplayed()
        {
            for(int i = 0; i < LetterDisplayers.Length; i++)
                if (!LetterDisplayers[i].IsSet)
                    return false;

            return true;
        }

        /// <summary>
        /// Sets a given letter to be displayed in the phrase.
        /// </summary>
        /// <param name="letter">The letter to be displayed in the phrase.</param>
        public virtual void SetLetter(char letter)
        {
            for(int i = 0; i < LetterDisplayers.Length; i++)
                //if(LetterDisplayers[i].Letter == letter)
                if(checkLetter(LetterDisplayers[i].Letter, letter))
                {
                    LetterDisplayers[i].SetLetterValue();
                }
        }

        char[] specialChar = {'Ž','ž','Ÿ','¡','¢','£','¤','¥','¦','§','¨','©','ª','À','Á','Â','Ã','Ä','Å','Æ','Ç','È','É','Ê','Ë','Ì','Í','Î','Ï','Ð','Ñ','Ò','Ó','Ô','Õ','Ö','Ù','Ú','Û','Ü','Ý','Þ','ß','à','á','â','ã','ä','å','æ','ç','è','é','ê','ë','ì','í','î','ï','ð','ñ','ò','ó','ô','õ','ö','ù','ú','û','ü','ý','þ','ÿ'};
        char[] specialA = {'À','Á','Â','Ã','Ä','Å','Æ'};
        char[] specialE = {'È','É','Ê','Ë'};
        char[] specialI = {'Ì','Í','Î','Ï'};
        char[] specialO = {'Ò','Ó','Ô','Õ','Ö'};
        char[] specialU = {'Ù','Ú','Û','Ü'};

        private Boolean checkLetter(char letterA, char letterB)
        {
            Boolean result = false;
            if (letterA == letterB)
                result = true;
            else
            {
                List<char> specialChar = new List<char>(this.specialChar);
                List<char> specialA = new List<char>(this.specialA);
                List<char> specialE = new List<char>(this.specialE);
                List<char> specialI = new List<char>(this.specialI);
                List<char> specialO = new List<char>(this.specialO);
                List<char> specialU = new List<char>(this.specialU);
                if( specialChar.Contains( letterA))
                {
                    if( letterA == 'Ç' && letterB == 'C')
                        result = true;
                    else if(specialA.Contains( letterA) && letterB == 'A')
                        result = true;
                    else if (specialE.Contains( letterA) && letterB == 'E')
                        result = true;
                    else if (specialI.Contains( letterA) && letterB == 'I')
                        result = true;
                    else if (specialO.Contains( letterA) && letterB == 'O')
                        result = true;
                    else if (specialU.Contains( letterA) && letterB == 'U')
                        result = true;
                }
            }
            Debug.Log(letterA + " " +result);
            return result;

        }

    }
}
