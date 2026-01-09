using System.Collections.ObjectModel;

using semantic_calculator.core.semantic_tree.Interface;

using SimpleWpf.ViewModel;

namespace semantic_calculator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<OperatorViewModel> Operators { get; private set; }
        public ObservableCollection<LogMessageViewModel> LogMessages { get; private set; }
        public ObservableCollection<LogMessageViewModel> CodeLines { get; private set; }

        public MainViewModel()
        {
            this.Operators = new ObservableCollection<OperatorViewModel>();
            this.LogMessages = new ObservableCollection<LogMessageViewModel>();
            this.CodeLines = new ObservableCollection<LogMessageViewModel>();
        }

        public void AddOperator(IOperator ioperator)
        {
            this.Operators.Add(new OperatorViewModel()
            {
                ArithmeticType = ioperator.ArithmeticType,
                Function = ioperator.Function,
                Order = ioperator.GetOrder(),
                Symbol = ioperator.GetSymbol()
            });
        }

        public void AddLog(string message, bool isError = false)
        {
            this.LogMessages.Add(new LogMessageViewModel()
            {
                Message = message,
                IsError = isError
            });
        }
        public void AddCodeLine(string line, bool isError = false)
        {
            this.CodeLines.Add(new LogMessageViewModel()
            {
                IsError = isError,
                Message = line
            });
        }
    }
}
