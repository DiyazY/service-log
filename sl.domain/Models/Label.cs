using bp.common;
using sl.domain.Exceptions;
using System.Collections.Generic;

namespace sl.domain.Models
{
    public sealed class Label : ValueObject
    {
        public string Value { get; private set; }

        #region constructors
        private Label()
        {

        }
        public Label(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new LabelDomainException("Empty label couldn't be added!");
            Value = value;
        }
        #endregion

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
