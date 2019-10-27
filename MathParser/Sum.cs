using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserClasses
{
    

    public class Sum:IFunction
    {
        public Sum()
        {
            Terms = new List<IFunction>();
        }
        public List<IFunction> Terms { get; set; }
        public double ComputeValue()
        {
            return Terms.Sum(p => p.ComputeValue());
        }
    }

    

    
   
    
    
    
    
    

    
}
