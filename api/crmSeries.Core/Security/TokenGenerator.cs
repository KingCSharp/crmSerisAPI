using System;
using System.Linq;
using System.Security.Cryptography;

namespace crmSeries.Core.Security
{
	public static class TokenGenerator
	{
		public static string Generate(DateTime timestamp, int keyLength = 16)
		{
			var key = new byte[keyLength];

			using (var generator = RandomNumberGenerator.Create())
			{
				generator.GetNonZeroBytes(key);
			}

			var time = BitConverter.GetBytes(timestamp.ToBinary());

			var token = Convert.ToBase64String(time.Concat(key).ToArray())
				.TrimEnd('=')
				.Replace('+', '-')
				.Replace('/', '_');

			return token;
		}
	}
}
