using System.Text;

namespace NKnife.Text
{
    public class StringJoiner
    {
        protected StringBuilder Builder;

        public StringJoiner()
        {
            Builder = new StringBuilder();
        }

        public static implicit operator StringJoiner(string value)
        {
            StringJoiner text = new StringJoiner();
            text.Builder.Append(value);
            return text;
        }

        public static StringJoiner operator +(StringJoiner self, string value)
        {
            self.Builder.Append(value);
            return self;
        }

        public static implicit operator string(StringJoiner value)
        {
            return value.ToString();
        }

        public override string ToString()
        {
            return this.Builder.ToString();
        }

    }
}
