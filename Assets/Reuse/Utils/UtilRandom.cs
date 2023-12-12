using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Reuse.Utils
{
    public static class UtilRandom
    {
        public static readonly Random RandomGenerator = new();
        public delegate T GetRandomValueDelegate<T>(T min, T max);

        public static int RandomIndex(int limit)
        {
            return RandomGenerator.Next(limit);
        }
        
        public static float GetRandomFloatInRange(double minNumber, double maxNumber)
        {
            return (float) (RandomGenerator.NextDouble() * (maxNumber - minNumber) + minNumber);
        }
        
        public static List<int> GetRandomNumberWithoutRepeating(int desiredAmount, int maxAmount)
        {
            List<int> randomIndexes = new List<int>();

            int maximumCount = desiredAmount > maxAmount ? maxAmount : desiredAmount;
            
            for (int i = 0; i < maximumCount; i++)
            {
                int index;

                int maxTentatives = 100;
                do 
                {
                    index = RandomGenerator.Next(maxAmount);
                    maxTentatives--;
                } 
                while (randomIndexes.Contains(index) && maxTentatives > 0);

                if (maxTentatives <= 0)
                {
                    Debug.LogError("I cannot believe, it repeated... count = " + maxAmount);
                }
                else
                {
                    randomIndexes.Add(index);
                }
            }
            return randomIndexes;
        }

        public static List<int> GetRandomNumbers(int desiredAmount, int maxAmount)
        {
            if (maxAmount == 0) return null;
            
            List<int> randomIndexes = new List<int>();

            int maximumValue = desiredAmount < maxAmount ? maxAmount : desiredAmount;
            
            for (var i = 0; i < desiredAmount; i++)
            {
                randomIndexes.Add(RandomIndex(maximumValue));
            }
            
            return randomIndexes;
        }
        
        public static void ShuffleArray<T>(ref T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = RandomIndex(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }
        

        public static void RandomVectorFill<T>(T[] array, int startIndex, T min, T max, GetRandomValueDelegate<T> getRandomValue)
        {
            for (int i = startIndex; i < array.Length; i++)
            {
                array[i] = getRandomValue(min, max);
            }
        }
        
        public static char[] GetRandomLetters(int desiredAmount, char[] dontInclude){
            var randomLetters = new char[desiredAmount];
            char character;
            for(int i = 0; i < desiredAmount; i++){
                do{
                    character = GetRandomLetter();
                }while(CharacterIsInside(dontInclude, character));

                randomLetters[i] = character;
            }
            return randomLetters;
        }

        public static char[] GetRandomLetters(int desiredAmount, char dontInclude){
            var randomLetters = new char[desiredAmount];
            char character;
            for(int i = 0; i < desiredAmount; i++){
                do{
                    character = GetRandomLetter();
                }while(character == dontInclude);

                randomLetters[i] = character;
            }
            return randomLetters;
        }

        public static char[] GetDifferentRandomLetters(int desiredAmount,char excludeChar){
            if(desiredAmount < 0 || desiredAmount > 26) return null;
            var randomLetters = new char[desiredAmount];
            var letters = new List<int>();
            var i = 0;

            for (i = 0; i < 26; i++)
            {
                letters.Add(i);
            }
            int index;
            for(i = 0; i < desiredAmount; i++){
                do{
                    index = RandomIndex(letters.Count);
                    randomLetters[i] = (char) (65 + letters[index]);
                }while(randomLetters[i] == excludeChar);

                letters.RemoveAt(index);
            }
            return randomLetters;
        }

        private static bool CharacterIsInside(char[] search, char character){
            for(var i = 0; i < search.Length; i++){
                if(search[i] == character) return true;
            }
            return false;
        }

        public static char GetRandomLetter(bool includeLowerCase = false){
            var increase = 0;
            if(includeLowerCase) increase = RandomIndex(2) * 32; 
            return (char) (RandomGenerator.Next(65, 91) + increase);
        }
    }
}