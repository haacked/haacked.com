using System;
using System.Text;
using System.Security.Cryptography;

namespace GU.Security.Cryptography
{
	/// <summary>
	/// Contains utility methods for easily calling Hashing algorithms.
	/// </summary>
	public sealed class HashLibrary
	{
		private HashLibrary()
		{
		}

		/// <summary>
		/// Returns an MD5 hash of the input string.
		/// </summary>
		/// <remarks>Assumes ASCII encoding.</remarks>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string MD5Hash(string input)
		{
			return MD5Hash(input, Encoding.ASCII);
		}

		/// <summary>
		/// Returns an MD5 hash of the input string using the 
		/// specified encoding.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string MD5Hash(string input, Encoding encoding)
		{
			return GetHash(new MD5CryptoServiceProvider(), input, encoding);
		}

		/// <summary>
		/// Returns an SHA1 hash of the input string.
		/// </summary>
		/// <remarks>Assumes ASCII encoding.</remarks>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string SHA1Hash(string input)
		{
			return SHA1Hash(input, Encoding.ASCII);
		}

		/// <summary>
		/// Returns an SHA1 hash of the input string using the 
		/// specified encoding.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string SHA1Hash(string input, Encoding encoding)
		{
			return GetHash(new SHA1CryptoServiceProvider(), input, encoding);
		}

		//Private utility method for doing the actual hashing.
		static string GetHash(HashAlgorithm algorithm, string input, Encoding encoding)
		{
			byte[] inputBuffer = encoding.GetBytes(input);
			byte[] result = algorithm.ComputeHash(inputBuffer);
			return BitConverter.ToString(result);
		}
	}
}
