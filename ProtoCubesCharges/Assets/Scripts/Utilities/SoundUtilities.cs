using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class SoundUtilities
    {
        /// <summary>
        /// Permet de régler le volume d'un audioMixer avec un slider de façon cohérent
        /// val min du slider = 0.0001
        /// val max du slider = 1
        /// </summary>
        
        public static float LinearToDecibel(this float linear)
        {
            float dB;

            if (linear != 0)
                dB = 20.0f * Mathf.Log10(linear);
            else
                dB = -144.0f;

            return dB;
        }

        public static float DecibelToLinear(this float dB)
        {
            float linear = Mathf.Pow(10.0f, dB / 20.0f);

            return linear;
        }
    }
}
