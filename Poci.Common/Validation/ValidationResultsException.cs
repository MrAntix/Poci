using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poci.Common.Validation
{
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
    }
}