using System;

namespace Assets.Scripts
{
	public class Randomizer
	{
		private readonly float halvedStayRange;
		private readonly Random random;
        private readonly float stepValue;
		private float levelRangeLower;
		private float levelRangeUpper;

        /// <summary>
        /// A component used to perform a randomwalk on a provided value within a range.
        /// </summary>
        /// <param name="initialLevel">The initial value on which the RandomWalk shall be performed.</param>
        /// <param name="totalMin">The absolute minimum that may be reached (usually given by the underlying component of the initial value).</param>
        /// <param name="totalMax">The absolute maximum that may be reached (usually given by the underlying component of the initial value).</param>
        /// <param name="rangeIncrementPercentage">The percentage of the total possible values that should be used to calculate the boundaries of the RandomWalk. 
        /// 10(%) means the boundaries will be set to initial value +-10% of the totally available values (totalMax-totalMin) and all values in between may be reached at some point.</param>
        /// <param name="stayProbability">The probability as percentage for the calculated value to stay the same on a RandomWalk-step.</param>
        /// <param name="rangeSplits">The amount of steps that should be possible within the calculated boundaries. This essentially defines the size of a possible in- or de-crementation on a RandomWalk-step.</param>
		public Randomizer (float initialLevel, float totalMin, float totalMax, float rangeIncrementPercentage = 10,
		                        float stayProbability = 30, int rangeSplits = 6)
		{
			random = new Random ();
			InitializeValueRanges (initialLevel, totalMin, totalMax, rangeIncrementPercentage);
			halvedStayRange = stayProbability * 100 / 2;
            stepValue = (levelRangeUpper - levelRangeLower) / rangeSplits;
		}

		private void InitializeValueRanges (float initialLevel, float min, float max, float incrementPercentage)
		{
            var increment = (max - min) * incrementPercentage / 100;
			levelRangeLower = GetLowerRangeValue (initialLevel, increment, min);
			levelRangeUpper = GetUpperRangeValue (initialLevel, increment, max);
		}

		private float GetUpperRangeValue (float currentValue, float increment, float maxValue)
		{
			var upperRange = currentValue + increment;
			if (upperRange > maxValue)
				upperRange = maxValue;
			return upperRange;
		}

		private float GetLowerRangeValue (float currentValue, float decrement, float minValue)
		{
			var lowerRange = currentValue - decrement;
			if (lowerRange < minValue)
				lowerRange = minValue;
			return lowerRange;
		}

        /// <summary>
        /// Calculates and applies the next value using the currentValue and the corresponding boundary as well as the globally defined limitations.
        /// </summary>
        /// <param name="currentValue">The value currently in use on which the RandomWalk shall be performed.</param>
        /// <returns></returns>
		public float DoRandomWalk (float currentValue)
		{            
            currentValue += stepValue * GetRandomWalkDirection(currentValue, levelRangeLower, levelRangeUpper);
			return currentValue;
		}

        /// <summary>
        /// 
        /// A random number between 0 and 9999 will be calculated and the currentValue will be normalized into that range. 
        /// Depending on where in that range the calculated value lands in comparison to the currentValue, this function will then return 0, +1 or -1
        /// </summary>
        /// <param name="currentValue">The value currently in use on which the RandomWalk shall be performed.</param>
        /// <param name="rangeLowerBound"></param>
        /// <param name="rangeUpperBound"></param>
        /// <returns></returns>
		private float GetRandomWalkDirection (float currentValue, float rangeLowerBound, float rangeUpperBound)
		{			
			var randomValue = random.Next (0, 9999);
			var currentLevelNormalized = (currentValue - rangeLowerBound) / (rangeUpperBound - rangeLowerBound) * 10000;

			if (randomValue < currentLevelNormalized - halvedStayRange)
				return -1;
			if (randomValue > currentLevelNormalized + halvedStayRange)
				return 1;
			return 0;
		}
	}
}