using System;
using System.Collections.Generic;
using System.Linq;

namespace DealOrNoDeal.Model
{
    /// <summary>Handles the management of the actual game play.</summary>
    public class GameManager
    {
        #region Fields

        private IList<Briefcase> briefcases;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the current round.
        /// </summary>
        /// <value>
        ///     The current round.
        /// </value>
        public int CurrentRound { get; set; }

        /// <summary>
        ///     Gets or sets the cases to open in round.
        /// </summary>
        /// <value>
        ///     The cases to open in round.
        /// </value>
        public int CasesToOpenInRound { get; set; }

        /// <summary>
        ///     Gets or sets the case selected.
        /// </summary>
        /// <value>
        ///     The case selected.
        /// </value>
        public int CaseSelected { get; set; }

        /// <summary>
        ///     Gets or sets the current offer.
        /// </summary>
        /// <value>
        ///     The current offer.
        /// </value>
        public int CurrentOffer { get; set; }

        /// <summary>
        ///     Gets or sets the minimum offer.
        /// </summary>
        /// <value>
        ///     The minimum offer.
        /// </value>
        public int MinOffer { get; set; }

        /// <summary>
        ///     Gets or sets the maximum offer.
        /// </summary>
        /// <value>
        ///     The maximum offer.
        /// </value>
        public int MaxOffer { get; set; }

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            this.briefcases = new List<Briefcase>();

            this.CurrentRound = 1;
            this.CasesToOpenInRound = 6;
            this.CaseSelected = -1;
            this.CurrentOffer = 0;
            this.MinOffer = int.MaxValue;
            this.MaxOffer = int.MinValue;
        }

        #endregion

        #region Methods
        
        /// <summary>
        ///     Populates the briefcases randomly.
        /// </summary>
        public void PopulateBriefcases()
        {
            var random = new Random();
            var populatedCases = new List<Briefcase>();

            var dollarAmounts = new List<int>() { 0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000, 750000, 1000000 };
            
            for (int i = 0; i < 26; i++)
            {
                var randomAmount = random.Next(0, dollarAmounts.Count);
                var dollarAmount = dollarAmounts[randomAmount];

                var currentBriefcase = new Briefcase(i, dollarAmount);

                populatedCases.Add(currentBriefcase);
                dollarAmounts.RemoveAt(randomAmount);
            }

            this.briefcases = populatedCases;
        }

        /// <summary>
        ///     Removes the specified briefcase from play.
        ///     Precondition: id > 0
        /// </summary>
        /// <param name="id">The id of the briefcase to remove from play.</param>
        /// <returns>Dollar amount stored in the case, or -1 if case not found.</returns>
        public int RemoveBriefcaseFromPlay(int id)
        {
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    this.briefcases.Remove(briefcase);
                    return briefcase.DollarAmount;
                }
            }

            return -1;
        }

        /// <summary>
        ///     Gets the brief case value from the briefcase id.
        ///     Precondition: id > 0
        ///     Postcondition: none
        /// </summary>
        /// <param name="id">The briefcase id to search.</param>
        /// <returns>the value of the briefcase from the id</returns>
        public int GetBriefCaseAmountById(int id)
        {
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    return briefcase.DollarAmount;
                }
            }

            return 0;
        }

        /// <summary>
        ///     Gets the offer.
        /// </summary>
        /// <returns>The current offer for the round.</returns>
        public int GetOffer()
        {
            var remainingDollarAmounts = this.briefcases.Select(item => item.DollarAmount).ToList();
            this.CurrentOffer = Banker.CalculateOfferFromBanker(remainingDollarAmounts, this.GetNumberOfCasesForRound(this.CurrentRound));

            return this.CurrentOffer;
        }

        /// <summary>
        ///     Sets the minimum offer.
        ///
        ///     Precondition: offers > 0
        ///     Postcondition: minimum offer has been found through the offers.
        /// </summary>
        /// <param name="offers">The list of offers.</param>
        /// <returns> the minimum offer </returns>
        public int GetMinimumOffer(List<int> offers)
        {
            foreach (var offer in offers)
            {
                if (this.MinOffer > offer)
                {
                    this.MinOffer = offer;

                }

            }

            return this.MinOffer;
        }

        /// <summary>
        ///     Gets the maximum offer.
        ///
        ///     Precondition: offers > 0
        ///     Postcondition: maximum offer has been found through the offers.
        /// </summary>
        /// <param name="offers">The list of offers.</param>
        /// <returns> the maximum offer </returns>
        public int GetMaximumOffer(List<int> offers)
        {
            foreach (var offer in offers)
            {
                if (offer > this.MaxOffer)
                {
                    this.MaxOffer = offer;
                }

            }

            return this.MaxOffer;
        }

        /// <summary>
        ///     Gets the number of cases for the round.
        ///
        ///     Precondition: currentRound >= 1
        ///     Postcondition: number of cases is retrieved based on the round.
        /// </summary>
        /// <param name="currentRound">The current round</param>
        /// <returns>The number of cases for that round.</returns>
        public int GetNumberOfCasesForRound(int currentRound)
        {
            List<int> casesFor10Rounds = new List<int> {
                6, 5, 4, 3, 2, 1, 1, 1, 1, 1
            };

            switch (currentRound)
            {
                case 1:
                    return casesFor10Rounds[0];
                case 2:
                    return casesFor10Rounds[1];
                case 3:
                    return casesFor10Rounds[2];
                case 4:
                    return casesFor10Rounds[3];
                case 5:
                    return casesFor10Rounds[4];
                case 6:
                    return casesFor10Rounds[5];
                case 7:
                    return casesFor10Rounds[6];
                case 8:
                    return casesFor10Rounds[7];
                case 9:
                    return casesFor10Rounds[8];
                case 10:
                    return casesFor10Rounds[9];
                default:
                    return 0;
            }

        }

        /// <summary>
        ///     Decreases the briefcases to be opened.
        /// </summary>
        public void DecreaseBriefcases()
        {
            this.CasesToOpenInRound--;
        }

        /// <summary>
        ///     Determines whether the round is over based on the number of cases.
        /// </summary>
        /// <returns> True if the number of cases left to open is 0; False otherwise</returns>
        public bool IsRoundOver()
        {
            return this.CasesToOpenInRound == 0;
        }

        /// <summary>
        ///     Determines the final round based on the current round.
        /// </summary>
        /// <returns>True if the current round is the final round; False otherwise</returns>
        public bool IsFinalRound()
        {
            return this.CurrentRound == 10;
        }

        /// <summary>
        ///     Moves to next round by incrementing Round property and setting
        ///     initial number of cases for that round
        ///     Precondition: None
        ///     Postcondition: Round == Round@prev + 1 AND CasesRemainingInRound == (number of cases to open in the next round)
        /// </summary>
        public void MoveToNextRound()
        {
            this.CurrentRound++;
            this.CasesToOpenInRound = this.GetNumberOfCasesForRound(this.CurrentRound);
        }

        #endregion

    }
}