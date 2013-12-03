using System;
using System.Collections.Specialized;

namespace GU.Text
{
	/// <summary>
	/// Contains useful string manipulation methods.
	/// </summary>
	public sealed class StringHelper
	{
		private StringHelper()
		{
		}

		/// <summary>
		/// Parses a camel cased or pascal cased string and returns an array 
		/// of the words within the string.
		/// </summary>
		/// <example>
		/// The string "PascalCasing" will return an array with two 
		/// elements, "Pascal" and "Casing".
		/// </example>
		/// <param name="source"></param>
		/// <returns></returns>
		public static string[] SplitUpperCase(string source)
		{
			if(source == null)
				return new string[] {}; //Return empty array.

			if(source.Length == 0)
				return new string[] {""};

			StringCollection words = new StringCollection();
			int wordStartIndex = 0;

			char[] letters = source.ToCharArray();
			// Skip the first letter. we don't care what case it is.
			for(int i = 1; i < letters.Length; i++)
			{
				if(char.IsUpper(letters[i]))
				{
					//Grab everything before the current index.
					words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
					wordStartIndex = i;
				}
			}
			//We need to have the last word.
			words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex)); 

			//Copy to a string array.
			string[] wordArray = new string[words.Count];
			words.CopyTo(wordArray, 0);
			return wordArray;
		}

		/// <summary>
		/// Parses a camel cased or pascal cased string and returns a new 
		/// string with spaces between the words in the string.
		/// </summary>
		/// <example>
		/// The string "PascalCasing" will return an array with two 
		/// elements, "Pascal" and "Casing".
		/// </example>
		/// <param name="source"></param>
		/// <returns></returns>
		public static string SplitUpperCaseToString(string source)
		{
			return string.Join(" ", SplitUpperCase(source));
		}

		/// <summary>
		/// Returns a string containing a specified number of characters from the left side of a string.
		/// </summary>
		/// <param name="str">Required. String expression from which the leftmost characters are returned.</param>
		/// <param name="length">Required. Integer greater than 0. Numeric expression 
		/// indicating how many characters to return. If 0, a zero-length string ("") 
		/// is returned. If greater than or equal to the number of characters in Str, 
		/// the entire string is returned. If str is null, this returns null.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if length is less than 0</exception>
		/// <exception cref="NullReferenceException">Thrown if str is null.</exception>
		public static string Left(string str, int length)
		{
			if(length >= str.Length)
				return str;

			return str.Substring(0, length);
		}

		/// <summary>
		/// Returns a string containing a specified number of characters from the right side of a string.
		/// </summary>
		/// <param name="str">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="length">Required. Integer greater than 0. Numeric expression 
		/// indicating how many characters to return. If 0, a zero-length string ("") 
		/// is returned. If greater than or equal to the number of characters in Str, 
		/// the entire string is returned. If str is null, this returns null.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if length is less than 0</exception>
		/// <exception cref="NullReferenceException">Thrown if str is null.</exception>
		public static string Right(string str, int length)
		{
			if(str == null)
				throw new NullReferenceException("Right cannot be evaluated on a null string.");

			if(length < 0)
				throw new ArgumentOutOfRangeException("length", length, "Length must not be negative.");
			
			if(str == "" || length == 0)
				return "";

			if(length >= str.Length)
				return str;

			return str.Substring(str.Length - length);
		}

		/// <summary>
		/// Returns a string containing every character within a string after the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="str">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="searchString">The string where the end of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException">Thrown if str or searchstring is null.</exception>
		public static string RightAfter(string str, string searchString)
		{
			return RightAfter(str, searchString, true);
		}

		/// <summary>
		/// Returns a string containing every character within a string after the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="str">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="searchString">The string where the end of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <param name="caseSensitive">Default true: If true, uses case sensitive search.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException">Thrown if str or searchstring is null.</exception>
		public static string RightAfter(string str, string searchString, bool caseSensitive)
		{
			if(searchString == null)
				throw new NullReferenceException("Search string may not be null.");

			//Shortcut.
			if(searchString.Length > str.Length || searchString.Length == 0)
				return str;

			int searchIndex;

			if(caseSensitive)
				searchIndex = str.IndexOf(searchString, 0);
			else
				searchIndex = str.ToUpper().IndexOf(searchString.ToUpper(), 0);
			
			if(searchIndex < 0)
				return str;

			return Right(str, str.Length - (searchIndex + searchString.Length));
		}

		/// <summary>
		/// Returns a string containing every character within a string before the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="str">Required. String expression from which the leftmost characters are returned.</param>
		/// <param name="searchString">The string where the beginning of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException">Thrown if str or searchstring is null.</exception>
		public static string LeftBefore(string str, string searchString)
		{
			return LeftBefore(str, searchString, true);
		}

		/// <summary>
		/// Returns a string containing every character within a string before the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="str">Required. String expression from which the leftmost characters are returned.</param>
		/// <param name="searchString">The string where the beginning of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <param name="caseSensitive">Default true: If true, uses case sensitive search.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException">Thrown if str or searchstring is null.</exception>
		public static string LeftBefore(string str, string searchString, bool caseSensitive)
		{
			if(searchString == null)
				throw new NullReferenceException("Search string may not be null.");

			//Shortcut.
			if(searchString.Length > str.Length || searchString.Length == 0)
				return str;

			int searchIndex;
			if(caseSensitive)
				searchIndex = str.IndexOf(searchString, 0);
			else
				searchIndex = str.ToUpper().IndexOf(searchString.ToUpper(), 0);

			if(searchIndex < 0)
				return str;

			return Left(str, searchIndex);
		}
	}
}
