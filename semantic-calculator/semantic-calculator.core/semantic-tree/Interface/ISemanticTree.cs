using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semantic_calculator.core.semantic_tree.Interface
{
    public interface ISemanticTree
    {
        void Load(string syntax);

        ISemanticTreeResult Parse();

        ISemanticTreeResult Execute();
    }
}
