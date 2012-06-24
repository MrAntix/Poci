using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Poci.Common.Validation
{
    [Serializable]
    public class ValidationResultsException
        : Exception
    {
        readonly IEnumerable<ValidationResult> _results;

        public ValidationResultsException(
            string message, IEnumerable<ValidationResult> results)
            : base(message)
        {
            _results = results;
        }

        public IEnumerable<ValidationResult> Results
        {
            get { return _results; }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("Results", _results);
            base.GetObjectData(info,context);
        }

    }
}