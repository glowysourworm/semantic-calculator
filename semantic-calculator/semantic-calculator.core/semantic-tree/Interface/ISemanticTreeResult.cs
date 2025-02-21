using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum SemanticTreeResultStatus
    {
        None=0, 
        Success=1, 
        SynataxError=2,
        ExecutionError = 3
    }

    public interface ISemanticTreeResult
    {
        public SemanticTreeResultStatus Status { get; }

    }
}
