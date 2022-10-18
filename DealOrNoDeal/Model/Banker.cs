using System;
using System.Collections.Generic;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     The Banker class.
    /// </summary>
    public class Banker
    {
        /// <summary>
        ///     Calculates the offer from banker.
        /// 
        ///     Precondition: remainingDollarAmounts > 0 && numberOfCasesToOpenNextRound > 0
        ///     Postcondition: the offer has been calculated
        /// </summary>
        /// <param name="remainingDollarAmounts">The remaining dollar amounts.</param>
        /// <param name="numberOfCasesToOpenNextRound">The number of cases to open next round.</param>
        /// <returns>The calculated offer from banker</returns>
        public static int CalculateOfferFromBanker(List<int> remainingDollarAmounts, int numberOfCasesToOpenNextRound)
        {
            var amountSum = 0.0;

            foreach (var amount in remainingDollarAmounts)
            {
                amountSum += amount;
            }

            var offer = amountSum / numberOfCasesToOpenNextRound / remainingDollarAmounts.Count;

            return (int)Math.Round(offer);
        }
    }
}
