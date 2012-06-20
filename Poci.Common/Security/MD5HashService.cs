using System;
using System.Security.Cryptography;
using System.Text;
using Poci.Common.Properties;

namespace Common.Security
{
    public class MD5HashService : IHashService
    {
        readonly MD5CryptoServiceProvider _service = new MD5CryptoServiceProvider();

        #region IHashService Members

        public string Hash(string value)
        {
            return Hash(value, false);
        }

        public string Hash64(string value)
        {
            return Hash(value, true);
        }

        #endregion

        string Hash(string value, bool base64Encode)
        {
            var bytes = _service.ComputeHash(
                Encoding.Default.GetBytes(
                    string.Concat(value, Settings.Default.SecurityHashSalt)));

            return base64Encode
                       ? Convert.ToBase64String(bytes)
                       : Encoding.Default.GetString(bytes);
        }
    }
}