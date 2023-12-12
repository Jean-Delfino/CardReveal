using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.CSV
{
    public class ComposedVersatileText : VersatileText
    {
        [SerializeField] private string[] nextTextKey;

        public override void SetText()
        {
            SetText(textKey);

            foreach (var item in nextTextKey)
            {
                IncreaseText(item);
            }
        } 
    }
}