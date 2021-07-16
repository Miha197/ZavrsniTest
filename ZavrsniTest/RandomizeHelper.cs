using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomize
{
    class RandomizeHelper
    {
		public static string GenerateRandomAlphanumeric(int length)
		{
			string randomAlphanumeric = GenerateRandomCharSequence(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");
			return randomAlphanumeric;
		}

		public static string GenerateRandomString(int length)
		{
			string randomString = GenerateRandomCharSequence(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
			return randomString;
		}

		public static string GenerateRandomNumber(int length)
		{
			string randomNumber = GenerateRandomCharSequence(length, "1234567890");
			return randomNumber;
		}

		private static string GenerateRandomCharSequence(int length, string possibleChars)
		{
			StringBuilder builder = new StringBuilder();
			Random rnd = new Random();

			while (builder.Length < length)
			{
				int index = (int)(rnd.NextDouble() * possibleChars.Length);
				builder.Append(possibleChars.ToCharArray().ElementAt(index));
			}
			return builder.ToString();
		}
	}
}
