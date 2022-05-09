namespace NHIValidation
{
    public static class NHIValidation
    {
        /// <summary>
        /// Checks to see if a string is a valid New Zealand National Health Index number
        /// </summary>
        /// <param name="nhi">the nhi number to validate</param>
        /// <returns>true is the nhi number is in a valid format</returns>
        public static bool IsNhiValid(string nhi)
        {
            nhi = nhi.ToUpper();    // we're going to accept nhis in lower case, but validate them as upper
            const string validNhiChars = "ABCDEFGHJKLMNPQRSTUVWXYZ";    // excludes I and O

            if (nhi.Length != 7) return false;

            // 1. Position 1,2 and 3 must be within the Alphabet Conversion Table(see above), that is, they are not ‘I’ or ‘O’.
            // suck it regex
            if (!nhi[..3].ToList().All(i => validNhiChars.Contains(i))) return false;

            // 2. Position 4 and 5 must be numeric
            if (!nhi[3..5].ToList().All(n => int.TryParse(n.ToString(), out _))) return false;

            // 3. Position 6 and 7 are either both numeric or both alphabetic
            var rule3 = nhi[5..7];
            if (!int.TryParse(rule3, out _) && rule3.Any(x => !x.IsEnglishLetter())) return false;

            // 4. Assign first letter its corresponding value from the Alphabet Conversion Table and multiply value by 7.
            // 5. Assign second letter its corresponding value from the Alphabet Conversion Table and multiply value by 6.
            // 6. Assign third letter its corresponding value from the Alphabet Conversion Table and multiply value by 5.
            // 7. Multiply first number by 4
            // 8. Multiply second number by 3.
            // 9. Multiply third number by 2
            // If the position 6 is an alpha character assign its corresponding value from the Alphabet Conversion Table and multiply value by 2
            // 10. Total the results of steps 4 to 9.
            var result = 0;
            int counter = 7;
            foreach (var character in nhi[..6])
            {
                if (character.IsEnglishLetter())
                {
                    result += counter * (validNhiChars.IndexOf(character) + 1);
                } else
                {
                    result += counter * (int)char.GetNumericValue(character);
                }
                counter--;
            }

            // 11. Apply modulus 11 to create a checksum.NB: Excel has a modulus function MOD(n, d) where n is the number to be
            // converted(eg, the sum calculated in step 9), and d equals the modulus (in the case of the NHI this is 11).
            // [If the position 6 is an alpha character]
            // Divide by 24 and get the remainder(in Java/ C#/javascript/etc , this is the(mod) %operator)
            var newFormat = nhi[5].IsEnglishLetter();
            int checkSum = result % (newFormat ? 24 : 11);

            // 12. If checksum is ‘0’ then the NHI number is incorrect.
            //[the document doesn't say, but this only applies to the old format]
            if (!newFormat && checkSum == 0) return false;

            // 13. Subtract checksum from 11 to create check digit.
            // [If the position 6 is an alpha character]
            // Subtract checksum from 24 and use the conversion table to create alpha check digit.
            string checkDigit = newFormat ? validNhiChars[(24 - checkSum - 1)].ToString() : (11 - checkSum).ToString();

            // 14. If the check digit equals ‘10’, convert to ‘0’.
            if(checkDigit == "10") checkDigit = "0";

            // 15. Fourth number or last character must equal the check digit.
            if (checkDigit != nhi[6].ToString()) return false;

            return true;
        }

        private static bool IsEnglishLetter(this char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

    }
}