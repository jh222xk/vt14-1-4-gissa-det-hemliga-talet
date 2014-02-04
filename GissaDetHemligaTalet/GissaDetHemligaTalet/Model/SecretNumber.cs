using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GissaDetHemligaTalet.Model
{
    public enum Outcome { Indefinite, Low, High, Correct, NoMoreGuesses, PreviousGuess }

    public class SecretNumber
    {
        // Fields
        private int _number;
        private List<int> _previousGuesses;

        // Constant
        private const int MaxNumberOfGuesses = 7;

        // Properties
        public bool CanMakeGuess
        {
            get { return Count < MaxNumberOfGuesses && !_previousGuesses.Contains(_number); }
        }

        public int Count
        {
            get { return _previousGuesses.Count(); }
        }

        public int? Number
        {
            get
            {
                // If we can make a guess, return null
                // otherwise return the number.
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
            private set
            {
                _number = (int)value;
            }
        }

        public Outcome Outcome { get; private set; }

        public ReadOnlyCollection<int> PreviousGuesses
        {
            get { return _previousGuesses.AsReadOnly(); }
        }

        // Constructor
        public SecretNumber()
        {
            List<int> PreviousGuesses = new List<int>();

            _previousGuesses = PreviousGuesses;

            Initialize();
        }

        // Methods
        public void Initialize()
        {
            Random rand = new Random();
            Number = rand.Next(1, 101);

            // Clear the previousguesses if there's any.
            if (PreviousGuesses.Count >= 1)
            {
                _previousGuesses.Clear();
            }

            Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            if (Count >= MaxNumberOfGuesses)
            {
                throw new System.ApplicationException("De blev lite för många gissningar...");
            }

            if (guess < 1 || guess > 100)
            {
                throw new System.ArgumentOutOfRangeException("Talet är inte mellan 1 och 100.");
            }

            if (Count >= 1)
            {
                if (PreviousGuesses.Contains(guess))
                {
                    return Outcome.PreviousGuess;
                }
            }

            // Add the guess to the list.
            _previousGuesses.Add(guess);

            if (guess == _number)
            {
                return Outcome.Correct;
            }

            if (Count == MaxNumberOfGuesses)
            {
                return Outcome.NoMoreGuesses;
            }

            if (guess < _number)
            {
                return Outcome.Low;
            }

            if (guess > _number)
            {
                return Outcome.High;
            }

            return Outcome.Indefinite;
        }
    }
}