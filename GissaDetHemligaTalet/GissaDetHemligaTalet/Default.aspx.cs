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
        protected void Page_Load(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = false;
            PlaceHolder2.Visible = false;
            Page.SetFocus(TextBoxNumber);

            if (Session["secretnumber"] == null)
            {
                SecretNumber newNumber = new SecretNumber();

                Session["secretnumber"] = newNumber;
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

                    SecretNumber newNumber = (SecretNumber)Session["secretnumber"];

                    Outcome outcome = newNumber.MakeGuess(number);

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
                        text = String.Format(" <img src='Content/tick.png' /> Grattis du klarade det på {0} försök.", newNumber.Count);
                        LabelResult.Text += text;
                        PlaceHolder2.Visible = true;
                    }
                    else if (outcome == Outcome.NoMoreGuesses)
                    {
                        text = String.Format(" <img src='Content/cross.png' /> Du har inga gissningar kvar. Det hemliga talet var {0}", newNumber.Number);
                        LabelResult.Text += text;
                        PlaceHolder2.Visible = true;
                    }
                    else if (outcome == Outcome.PreviousGuess)
                    {
                        text = String.Format(" <img src='Content/error.png' /> Du har redan gissat på talet.");
                        LabelResult.Text += text;
                    }

                    if (!newNumber.CanMakeGuess)
                    {
                        ButtonCheckNumber.Enabled = false;
                        TextBoxNumber.Enabled = false;
                        Page.SetFocus(ButtonGenerateNewNumber);

                    }

                    // Iterate through the list of previousguesses.
                    foreach (var item in newNumber.PreviousGuesses)
                    {
                        LabelPreviousNumbers.Text = string.Join(", ", newNumber.PreviousGuesses);
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
            // Clear the current session...
            Session.Clear();

            // ... and redirect the user to the same page.
            Response.Redirect(Request.RawUrl);
        }
    }
}