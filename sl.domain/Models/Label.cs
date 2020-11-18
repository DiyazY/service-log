using sl.domain.Exceptions;

namespace sl.domain.Models
{
    public sealed record Label
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
    }
}
