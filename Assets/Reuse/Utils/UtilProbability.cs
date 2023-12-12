using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilProbability
    {
        public delegate int MinimumFinderDelegate<in T>(int[] numbers, T extraData);

        public class ProcDefinition
        {
            public float[] ProcChances;
            public float[] DefaultProcChances;
            public int[] Proc;
            public int AmountOfProc;
        }

        public static void AddProcCount(ProcDefinition procDefinition, int index)
        {
            procDefinition.Proc[index]++;
            procDefinition.AmountOfProc++;
        }

        public static int GetProcInProcDefinition(ProcDefinition procDefinition)
        {
            var chance = UtilRandom.RandomGenerator.NextDouble();
            var actualChance = 0f;
            
            for (int i = 0; i < procDefinition.ProcChances.Length; i++)
            {
                actualChance += procDefinition.ProcChances[i];
                if (actualChance >= chance) return i;
            }

            return -1; //This is impossible with correct configuration and code
        }

        public static void StartDefaultProcChances(ProcDefinition procDefinition)
        {
            int size = procDefinition.ProcChances.Length;

            for (int i = 0; i < size; i++)
            {
                procDefinition.ProcChances[i] = procDefinition.DefaultProcChances[i] = (1.0f) / size;
                
            }
        }

        public static bool StartDefaultProcChances(ProcDefinition procDefinition, float[] chances)
        {
            int size = chances.Length;
            if (size != procDefinition.DefaultProcChances.Length)
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                procDefinition.ProcChances[i] = procDefinition.DefaultProcChances[i] = chances[i];
            }

            return true;
        }

        public static ProcDefinition GenerateProc(int amount)
        {
            return new ProcDefinition()
            {
                ProcChances = new float[amount],
                DefaultProcChances = new float[amount],
                Proc = new int[amount],
                AmountOfProc = 0
            };
        }
        
        public static void ChangeProcConfigurationBasedOnProcAndDivisor<T>(ProcDefinition procDefinition, 
            T extraData, 
            float divisor = 2, 
            MinimumFinderDelegate<T> minimumFinderDelegate = null)
        {
            var procAmounts = procDefinition.Proc;
            var min = minimumFinderDelegate?.Invoke(procAmounts, extraData) ?? BruteForceFindMin(procDefinition.Proc);

            List<int> indexNormal = new();
            int multiplier = procDefinition.AmountOfProc > 0 ? procDefinition.AmountOfProc : 1; //Safe check, but if this happen the else shouldn't happen, unless there's error
            float sumOfAbnormalProc = 0f;
            
            for (int i = 0; i < procAmounts.Length; i++)
            { 
                if(procAmounts[i] == min) indexNormal.Add(i);
                else
                {
                    sumOfAbnormalProc = sumOfAbnormalProc + (procDefinition.ProcChances[i] = 
                        procDefinition.DefaultProcChances[i] / (divisor * ((procAmounts[i] - min) * multiplier)));
                }
            }

            float procForNormal = (1 - sumOfAbnormalProc) / (indexNormal.Count);

            foreach (var index in indexNormal)
            {
                procDefinition.ProcChances[index] = procForNormal;
            }
        }

        public static int DefaultProcGeneration(ProcDefinition procDefinition)
        {
            var index = GetProcInProcDefinition(procDefinition);
            AddProcCount(procDefinition, index);

            ChangeProcConfigurationBasedOnProcAndDivisor<Object>( procDefinition, null);

            return index;
        }

        private static int BruteForceFindMin(int[] proc)
        {
            int min = proc[0];

            for (int i = 1; i < proc.Length; i++)
            {
                if (proc[i] < min) min = proc[i];
            }

            return min;
        }
    }
}