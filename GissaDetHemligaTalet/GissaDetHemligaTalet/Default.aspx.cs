using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GissaDetHemligaTalet.Model;

namespace GissaDetHemligaTalet
{
    public partial class Default : System.Web.UI.Page
    {
        private SecretNumber SecretNumber
        {
            get { return Session["secretnumber"] as SecretNumber; }
            set
            {
                Session["secretnumber"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.SetFocus(TextBoxNumber);

            if (SecretNumber == null)
            {
                SecretNumber newNumber = new SecretNumber();

                SecretNumber = newNumber;
            }
        }

        protected void ButtonCheckNumber_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    int number;
                    string text;

                    number = Int32.Parse(TextBoxNumber.Text);

                    Outcome outcome = SecretNumber.MakeGuess(number);

                    if (outcome == Outcome.High)
                    {
                        text = String.Format(" <img src='Content/arrow_up.png' /> För högt.");
                        LabelResult.Text += text;
                    }
                    else if (outcome == Outcome.Low)
                    {
                        text = String.Format(" <img src='Content/arrow_down.png' /> För lågt.");
                        LabelResult.Text += text;
                    }
                    else if (outcome == Outcome.Correct)
                    {
                        text = String.Format(" <img src='Content/tick.png' /> Grattis du klarade det på {0} försök.", SecretNumber.Count);
                        LabelResult.Text += text;
                        PlaceHolder2.Visible = true;
                    }
                    else if (outcome == Outcome.NoMoreGuesses)
                    {
                        text = String.Format(" <img src='Content/cross.png' /> Du har inga gissningar kvar. Det hemliga talet var {0}", SecretNumber.Number);
                        LabelResult.Text += text;
                        PlaceHolder2.Visible = true;
                    }
                    else if (outcome == Outcome.PreviousGuess)
                    {
                        text = String.Format(" <img src='Content/error.png' /> Du har redan gissat på talet.");
                        LabelResult.Text += text;
                    }

                    if (!SecretNumber.CanMakeGuess)
                    {
                        ButtonCheckNumber.Enabled = false;
                        TextBoxNumber.Enabled = false;
                        Page.SetFocus(ButtonGenerateNewNumber);

                    }

                    // Iterate through the list of previousguesses.
                    foreach (var item in SecretNumber.PreviousGuesses)
                    {
                        LabelPreviousNumbers.Text = string.Join(", ", SecretNumber.PreviousGuesses);
                    }

                    PlaceHolder1.Visible = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }

            }
        }

        protected void ButtonGenerateNewNumber_Click(object sender, EventArgs e)
        {
            SecretNumber.Initialize();
            Response.Redirect(Request.RawUrl);
        }
    }
}