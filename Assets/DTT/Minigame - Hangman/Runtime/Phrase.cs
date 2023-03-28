using System.Collections.Generic;
using System;
using System.Linq;

namespace DTT.Hangman
{
   /// <summary>
   /// Represents a phrase usable for a hangman game.
   /// </summary>
   [Serializable]
   public struct Phrase
   {
      /// <summary>
      /// The phrase value.
      /// </summary>
      public string value;

      /// <summary>
      /// The descriptions of the phrase usable for hints.
      /// </summary>
      public string[] descriptions;
      
      /// <summary>
      /// The exposed letters of the phrase to be shown
      /// at the start of the game.
      /// </summary>
      public char[] exposedLetters;

      /// <summary>
      /// Returns a character of the phrase based on the character element index.
      /// </summary>
      /// <param name="index">The index.</param>
      public char this[int index] => value[index];
      
      /// <summary>
      /// The length of the phrase.
      /// </summary>
      public int Length => value.Length;

      /// <summary>
      /// The amount of words in the phrase.
      /// </summary>
      public int WordCount => value.Count(letter => letter == ' ') + 1;

      /// <summary>
      /// Creates a new phrase instance.
      /// </summary>
      /// <param name="value">The phrase value.</param>
      /// <param name="descriptions">The exposed letters of the phrase to be shown at the start of the game.</param>
      /// <param name="exposedLetters">The exposed letters of the phrase to be shown at the start of the game.</param>
      public Phrase(string value, string[] descriptions = null, char[] exposedLetters = null)
      {
         this.value = value;
         this.descriptions = descriptions;
         this.exposedLetters = exposedLetters;
      }

      /// <summary>
      /// Splits the phrase up in sub phrases. Use this to split up a phrase that is a sentence into words.
      /// </summary>
      /// <returns>The sub phrases.</returns>
      public Phrase[] Split()
      {
         Phrase[] phrases = value.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries)
            .Select(phrase => new Phrase(phrase))
            .ToArray();
         
         for (int i = 0; i < phrases.Length; i++)
         {
            phrases[i].descriptions = descriptions;
            phrases[i].exposedLetters = exposedLetters;
         }

         return phrases;
      }

      /// <summary>
      /// Returns a new phrase that exposes the given letters.
      /// </summary>
      /// <param name="exposedLetters">The letters to expose.</param>
      /// <returns>The new phrase exposing the letters.</returns>
      public Phrase Expose(char[] exposedLetters) => new Phrase(value, descriptions, exposedLetters);

      /// <summary>
      /// Returns whether this phrase contains a given letter.
      /// </summary>
      /// <param name="letter">The letter to check for.</param>
      /// <returns>Whether this phrase contains a given letter.</returns>
      public bool Contains(char letter)
      {
         char letterToUpper = char.ToUpperInvariant(letter);
         LetterChecker checker = new LetterChecker();
         for(int i = 0; i < value.Length; i++)
            //if (char.ToUpperInvariant(value[i]) == letterToUpper)
            if (checker.checkLetter(char.ToUpperInvariant(value[i]), letterToUpper))
            {
               return true;
            }

         return false;
      }

      /// <summary>
      /// Returns whether a given letter is exposed by this phrase.
      /// </summary>
      /// <param name="letter">The letter to check for.</param>
      /// <returns>Whether the given letter is exposed by this phrase.</returns>
      public bool Exposes(char letter)
      {
         char letterToUpper = char.ToUpperInvariant(letter);
         for(int i = 0; i < exposedLetters.Length; i++)
            if (char.ToUpperInvariant(exposedLetters[i]) == letterToUpper)
            {
               return true;
            }

         return false;
      }

      /// <summary>
      /// Returns the string value of this phrase.
      /// </summary>
      /// <returns>The string value.</returns>
      public override string ToString() => value;
   }

   public class LetterChecker{
      char[] specialChar = {'Ž','ž','Ÿ','¡','¢','£','¤','¥','¦','§','¨','©','ª','À','Á','Â','Ã','Ä','Å','Æ','Ç','È','É','Ê','Ë','Ì','Í','Î','Ï','Ð','Ñ','Ò','Ó','Ô','Õ','Ö','Ù','Ú','Û','Ü','Ý','Þ','ß','à','á','â','ã','ä','å','æ','ç','è','é','ê','ë','ì','í','î','ï','ð','ñ','ò','ó','ô','õ','ö','ù','ú','û','ü','ý','þ','ÿ'};
      char[] specialA = {'À','Á','Â','Ã','Ä','Å','Æ'};
      char[] specialE = {'È','É','Ê','Ë'};
      char[] specialI = {'Ì','Í','Î','Ï'};
      char[] specialO = {'Ò','Ó','Ô','Õ','Ö'};
      char[] specialU = {'Ù','Ú','Û','Ü'};

      public Boolean checkLetter(char letterA, char letterB)
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
         return result;

      }
   }
}
