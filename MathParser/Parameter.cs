namespace MathParser
{
    public class Parameter
    {
        public string VariableName { get; set; }

        public double Value { get; set; }

        public Variable Variable { 
            get
            {
                return new Variable { Name = VariableName };
            } 
        }
    }
}