using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Pattern
{
    public class MainNamedEventPublisher : MonoBehaviour
    {
        private static NamedEventPublisher instance;
        public static NamedEventPublisher Instance {
            get{
                if(instance == null) instance = FindObjectOfType<NamedEventPublisher>();

                return instance;
            }
        }


    }
}
