namespace System.ComponentModel.DataAnnotations
{
    public class InsertDefaultAttribute : Attribute
    {
        public InsertDefaultAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public string DefaultValue { get; set; }
    }
}