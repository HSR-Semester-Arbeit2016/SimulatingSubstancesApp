using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Randomizer
    {
        private readonly Random random;
        private readonly float halvedStayRange;
        private readonly int rangeSplits;
        private float levelRangeLower;
        private float levelRangeUpper;
        
        public Randomizer(float initialLevel, float totalMin, float totalMax, float rangeIncrement, float stayRange = 3000, int rangeSplits = 6)
        {
            random = new Random();
            InitializeValueRanges(initialLevel, totalMin, totalMax, rangeIncrement);
            halvedStayRange = stayRange/2;
            this.rangeSplits = rangeSplits;
        }

        private void InitializeValueRanges(float initialLevel, float min, float max, float increment)
        {
            levelRangeLower = GetLowerRangeValue(initialLevel, increment, min);
            levelRangeUpper = GetUpperRangeValue(initialLevel, increment, max);
        }

        private float GetUpperRangeValue(float currentValue, float increment, float maxValue)
        {
            var upperRange = currentValue + increment;
            if (upperRange > maxValue)
                upperRange = maxValue;
            return upperRange;
        }

        private float GetLowerRangeValue(float currentValue, float decrement, float minValue)
        {
            var lowerRange = currentValue - decrement;
            if (lowerRange < minValue)
                lowerRange = minValue;
            return lowerRange;
        }

        public float DoRandomWalk(float currentValue)
        {
            currentValue += GetRandomWalkDirection(currentValue, levelRangeLower, levelRangeUpper);
            return currentValue;
        }

        private float GetRandomWalkDirection(float currentValue, float rangeLowerBound, float rangeUpperBound)
        {
            float stepValue = (rangeUpperBound - rangeLowerBound) / rangeSplits;
            int randomValue = random.Next(0, 9999);
            float currentLevelNormalized = (currentValue - rangeLowerBound) / (rangeUpperBound - rangeLowerBound) * 10000;

            if (randomValue < currentLevelNormalized - halvedStayRange)
                return -stepValue;
            else if (randomValue > currentLevelNormalized + halvedStayRange)
                return stepValue;
            else
                return 0;

        }
    }
}
