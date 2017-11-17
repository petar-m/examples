namespace Sentences
{
    public class Word
    {
        protected Word()
        {
        }

        public Word(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
