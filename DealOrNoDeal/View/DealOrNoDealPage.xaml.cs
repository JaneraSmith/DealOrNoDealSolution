using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using DealOrNoDeal.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DealOrNoDeal.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DealOrNoDealPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 500;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 500;

        private IList<Button> briefcaseButtons;
        private IList<Border> dollarAmountLabels;

        private readonly GameManager gameManager;
        private readonly List<int> offers;

        private int finalCaseId;
        private int finalCaseAmount;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DealOrNoDealPage"/> class.
        /// </summary>
        public DealOrNoDealPage()
        {
            this.InitializeComponent();
            this.initializeUiDataAndControls();
            this.gameManager = new GameManager();

            this.gameManager.PopulateBriefcases();
            this.hideDealAndNoDealButtons();

            this.offers = new List<int>();
        }

        #endregion

        #region Methods

        private void initializeUiDataAndControls()
        {
            this.setPageSize();

            this.briefcaseButtons = new List<Button>();
            this.dollarAmountLabels = new List<Border>();
            this.buildBriefcaseButtonCollection();
            this.buildDollarAmountLabelCollection();
        }

        private void setPageSize()
        {
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

        private void buildDollarAmountLabelCollection()
        {
            this.dollarAmountLabels.Clear();

            this.dollarAmountLabels.Add(this.label0Border);
            this.dollarAmountLabels.Add(this.label1Border);
            this.dollarAmountLabels.Add(this.label2Border);
            this.dollarAmountLabels.Add(this.label3Border);
            this.dollarAmountLabels.Add(this.label4Border);
            this.dollarAmountLabels.Add(this.label5Border);
            this.dollarAmountLabels.Add(this.label6Border);
            this.dollarAmountLabels.Add(this.label7Border);
            this.dollarAmountLabels.Add(this.label8Border);
            this.dollarAmountLabels.Add(this.label9Border);
            this.dollarAmountLabels.Add(this.label10Border);
            this.dollarAmountLabels.Add(this.label11Border);
            this.dollarAmountLabels.Add(this.label12Border);
            this.dollarAmountLabels.Add(this.label13Border);
            this.dollarAmountLabels.Add(this.label14Border);
            this.dollarAmountLabels.Add(this.label15Border);
            this.dollarAmountLabels.Add(this.label16Border);
            this.dollarAmountLabels.Add(this.label17Border);
            this.dollarAmountLabels.Add(this.label18Border);
            this.dollarAmountLabels.Add(this.label19Border);
            this.dollarAmountLabels.Add(this.label20Border);
            this.dollarAmountLabels.Add(this.label21Border);
            this.dollarAmountLabels.Add(this.label22Border);
            this.dollarAmountLabels.Add(this.label23Border);
            this.dollarAmountLabels.Add(this.label24Border);
            this.dollarAmountLabels.Add(this.label25Border);
        }

        private void buildBriefcaseButtonCollection()
        {
            this.briefcaseButtons.Clear();

            this.briefcaseButtons.Add(this.case0);
            this.briefcaseButtons.Add(this.case1);
            this.briefcaseButtons.Add(this.case2);
            this.briefcaseButtons.Add(this.case3);
            this.briefcaseButtons.Add(this.case4);
            this.briefcaseButtons.Add(this.case5);
            this.briefcaseButtons.Add(this.case6);
            this.briefcaseButtons.Add(this.case7);
            this.briefcaseButtons.Add(this.case8);
            this.briefcaseButtons.Add(this.case9);
            this.briefcaseButtons.Add(this.case10);
            this.briefcaseButtons.Add(this.case11);
            this.briefcaseButtons.Add(this.case12);
            this.briefcaseButtons.Add(this.case13);
            this.briefcaseButtons.Add(this.case14);
            this.briefcaseButtons.Add(this.case15);
            this.briefcaseButtons.Add(this.case16);
            this.briefcaseButtons.Add(this.case17);
            this.briefcaseButtons.Add(this.case18);
            this.briefcaseButtons.Add(this.case19);
            this.briefcaseButtons.Add(this.case20);
            this.briefcaseButtons.Add(this.case21);
            this.briefcaseButtons.Add(this.case22);
            this.briefcaseButtons.Add(this.case23);
            this.briefcaseButtons.Add(this.case24);
            this.briefcaseButtons.Add(this.case25);

            this.storeBriefCaseIndexInControlsTagProperty();
        }

        private void storeBriefCaseIndexInControlsTagProperty()
        {
            for (var i = 0; i < this.briefcaseButtons.Count; i++)
            {
                this.briefcaseButtons[i].Tag = i;
            }
        }

        private void briefcase_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            clickedButton.IsEnabled = false;
            clickedButton.Visibility = Visibility.Collapsed;

            var clickedButtonId = this.getBriefcaseId(clickedButton);

            if (this.gameManager.CaseSelected == -1)
            {
                this.setPlayerCaseSelection(clickedButtonId, clickedButton);
            }
            else
            {
                this.setupBriefcaseSelection(clickedButtonId);
            }
            this.updateCurrentRoundInformation();
            
        }

        private void setupBriefcaseSelection(int clickedButtonId)
        {
            this.findAndGrayOutGameDollarLabel(this.gameManager.GetBriefCaseAmountById(clickedButtonId));
            this.gameManager.RemoveBriefcaseFromPlay(clickedButtonId);
            this.gameManager.DecreaseBriefcases();
        }

        private void findAndGrayOutGameDollarLabel(int amount)
        {
            foreach (var currDollarAmountLabel in this.dollarAmountLabels)
            {
                if (grayOutLabelIfMatchesDollarAmount(amount, currDollarAmountLabel))
                {
                    break;
                }
            }
        }

        private static bool grayOutLabelIfMatchesDollarAmount(int amount, Border currDollarAmountLabel)
        {
            var matched = false;

            if (currDollarAmountLabel.Child is TextBlock dollarTextBlock)
            {
                var labelAmount = int.Parse(dollarTextBlock.Text, NumberStyles.Currency);
                if (labelAmount == amount)
                {
                    currDollarAmountLabel.Background = new SolidColorBrush(Colors.Gray);
                    matched = true;
                }
            }

            return matched;
        }

        private void setPlayerCaseSelection(int clickedButtonId, Button clickedButton)
        {
            this.gameManager.CaseSelected = clickedButtonId;
            this.summaryOutput.Text = "Your Case: " + clickedButton.Content;
        }
        
        private int getBriefcaseId(Button selectedBriefCase)
        {
            return (int)selectedBriefCase.Tag;
        }

        private void updateCurrentRoundInformation()
        {
            if (this.gameManager.IsFinalRound())
            {
                this.setupFinalRound();
            }
            else
            {
                this.setupCurrentRoundText();

                this.gameManager.CurrentOffer = this.gameManager.GetOffer();

                if (this.gameManager.IsRoundOver())
                {
                    this.setupRoundOver();
                }
            }
            
        }

        private void setupCurrentRoundText()
        {
            this.enableBriefCases();
            this.hideDealAndNoDealButtons();

            this.updateRoundLabel();

            this.updateCasesToOpenLabel();

            
        }

        private void updateCasesToOpenLabel()
        {
            this.casesToOpenLabel.Text = $"{this.gameManager.CasesToOpenInRound}";

            if (this.gameManager.CasesToOpenInRound == 1)
            {
                this.casesToOpenLabel.Text += " more case to open";
            }
            else
            {
                this.casesToOpenLabel.Text += " more cases to open";
            }
        }

        private void updateRoundLabel()
        {
            this.roundLabel.Text =
                $"Round {this.gameManager.CurrentRound}: {this.gameManager.GetNumberOfCasesForRound(this.gameManager.CurrentRound)}";

            if (this.gameManager.GetNumberOfCasesForRound(this.gameManager.CurrentRound) == 1)
            {
                this.roundLabel.Text += " case to open";
            }
            else
            {
                this.roundLabel.Text += " cases to open";
            }
        }

        private void setupFinalRound()
        {
            this.displayDealAndNoDealButtons();

            this.roundLabel.Text = "This is the final round.";
            this.casesToOpenLabel.Text = "Select a case.";

            this.dealButton.Content = $"Case {this.gameManager.CaseSelected + 1}";

            this.finalCaseId = (int)this.getFinalBriefcaseButton().Tag + 1;
            this.finalCaseAmount = this.getFinalBriefcaseAmount();
            this.noDealButton.Content = $"Case {this.finalCaseId}";

            this.getFinalBriefcaseButton().Visibility = Visibility.Collapsed;
        }

        private void setupRoundOver()
        {
            this.offers.Add(this.gameManager.CurrentOffer);
            this.gameManager.MinOffer = this.gameManager.GetMinimumOffer(this.offers);
            this.gameManager.MaxOffer = this.gameManager.GetMaximumOffer(this.offers);

            this.summaryOutput.Text =
                $"Offers: Min: {this.gameManager.MinOffer:C}; Max: {this.gameManager.MaxOffer:C}" +
                Environment.NewLine + $"Current Offer: {this.gameManager.CurrentOffer:C}" + Environment.NewLine +
                "Deal or No Deal?";
            this.disableBriefCases();
            this.displayDealAndNoDealButtons();
        }

        private void updateCurrentRoundInformationWithLastOffer()
        {
            this.summaryOutput.Text =
                $"Offers: Min: {this.gameManager.MinOffer:C}; Max: {this.gameManager.MaxOffer:C}{Environment.NewLine} Last offer: {this.gameManager.CurrentOffer:C}";

            if (this.gameManager.IsFinalRound())
            {
                this.summaryOutput.Text = $"Offers: Min: {this.gameManager.MinOffer:C}; Max: {this.gameManager.MaxOffer:C}";
                    
            }
        }

        private void hideDealAndNoDealButtons()
        {
            this.dealButton.Visibility = Visibility.Collapsed;
            this.noDealButton.Visibility = Visibility.Collapsed;
        }

        private void displayDealAndNoDealButtons()
        {
            this.dealButton.Visibility = Visibility.Visible;
            this.noDealButton.Visibility = Visibility.Visible;
        }

        private void enableBriefCases()
        {
            foreach (var briefcaseButton in this.briefcaseButtons)
            {
                briefcaseButton.IsEnabled = true;
            }
        }

        private void disableBriefCases()
        {
            foreach (var briefcaseButton in this.briefcaseButtons)
            {
                briefcaseButton.IsEnabled = false;
            }
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.gameManager.IsFinalRound())
            {
                this.displayAcceptedDealAmount();
                this.displayPlayAgain();
            }
            else
            {
                this.displayAcceptedPlayerCaseAmount();
                
                this.displayPlayAgain();
            }
        }

        private void displayAcceptedPlayerCaseAmount()
        {
            this.summaryOutput.Text =
                $"Congrats you win {this.gameManager.GetBriefCaseAmountById(this.gameManager.CaseSelected):C}" +
                Environment.NewLine + Environment.NewLine + "GAME OVER";
            this.hideDealAndNoDealButtons();
        }

        private void displayAcceptedDealAmount()
        {
            this.hideDealAndNoDealButtons();
            this.summaryOutput.Text =
                $"Your case contained: {this.gameManager.GetBriefCaseAmountById(this.gameManager.CaseSelected):C}" +
                Environment.NewLine + $"Accepted offer: {this.gameManager.GetOffer():C}" +
                Environment.NewLine + "GAME OVER";
        }

        private void noDealButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.gameManager.IsFinalRound())
            {
                this.continueToNextRound();
            }
            else
            {
                this.displayAcceptedFinalCaseAmount();

                this.displayPlayAgain();
            }

        }

        private void displayAcceptedFinalCaseAmount()
        {
            this.summaryOutput.Text = $"Congrats you win {this.finalCaseAmount:C}" + Environment.NewLine + Environment.NewLine + "GAME OVER";
            this.hideDealAndNoDealButtons();
        }

        private void continueToNextRound()
        {
            this.gameManager.MoveToNextRound();
            this.updateCurrentRoundInformationWithLastOffer();
            this.updateCurrentRoundInformation();
        }

        private Button getFinalBriefcaseButton()
        {
            Button finalButton = null;
            foreach (var briefcaseButton in this.briefcaseButtons)
            {
                if (briefcaseButton.Visibility == Visibility.Visible)
                {
                    finalButton = briefcaseButton;
                }
            }
            return finalButton;
        }

        private int getFinalBriefcaseAmount()
        {
            int finalCaseAmount = 0;

            if (this.getFinalBriefcaseButton().Visibility == Visibility.Visible)
            {
                finalCaseAmount = this.gameManager.GetBriefCaseAmountById(this.getBriefcaseId(this.getFinalBriefcaseButton()));
            }
            return finalCaseAmount;
        }

        private async void displayPlayAgain()
        {
            var contentDialog = new ContentDialog()
            {
                Title = "Game Over",
                Content = "Would you like to play again?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            var result = await contentDialog.ShowAsync();

            switch (result)
            {
                case ContentDialogResult.Primary:
                    await CoreApplication.RequestRestartAsync("");
                    break;
                case ContentDialogResult.Secondary:
                    Application.Current.Exit();
                    break;
            }
        }

        #endregion
    }
}