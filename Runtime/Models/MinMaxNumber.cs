﻿using System;
using Random = UnityEngine.Random;

namespace OmegaLeo.Toolbox.Runtime.Models
{
    [Serializable]
    public class MinMaxNumber
    {
        public float Min;
        public float Max;

        public MinMaxNumber(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public MinMaxNumber() : this(0, 10) {}

        // Implicit conversion to float
        public static implicit operator float(MinMaxNumber number)
        {
            return Random.Range(number.Min, number.Max);
        }

        // Implicit conversion to int
        public static implicit operator int(MinMaxNumber number)
        {
            return Random.Range((int)number.Min, (int)number.Max + 1);
        }
    }
}