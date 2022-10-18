using System;

namespace DealOrNoDeal.Model
{

    /// <summary>
    ///     The Briefcase class.
    /// </summary>
    public class Briefcase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the briefcase Id.
        /// </summary>
        /// <value>
        ///     The briefcase Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the dollar amount.
        /// </summary>
        /// <value>
        ///     The dollar amount.
        /// </value>
        public int DollarAmount { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Briefcase"/> class.
        /// </summary>
        /// <param name="id">The id of the briefcase.</param>
        /// <param name="dollarAmount">The dollar amount.</param>
        public Briefcase(int id, int dollarAmount)
        {
            if (id < 0 || id > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            if (dollarAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dollarAmount));
            }

            this.Id = id;
            this.DollarAmount = dollarAmount;
        }

        #endregion
    }
}
