namespace System.ComponentModel.DataAnnotations
{
    public class UpdateDefaultAttribute : Attribute
    {
        public UpdateDefaultAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public string DefaultValue { get; set; }
    }
}