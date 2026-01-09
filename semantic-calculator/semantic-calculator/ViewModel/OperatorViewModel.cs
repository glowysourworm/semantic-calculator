using semantic_calculator.core.semantic_tree.Interface;

using SimpleWpf.ViewModel;

namespace semantic_calculator.ViewModel
{
    public class OperatorViewModel : ViewModelBase
    {
        string _symbol;
        uint _order;
        OperatorArithmeticType _arithmeticType;
        OperatorFunction _function;

        public string Symbol
        {
            get { return _symbol; }
            set { this.RaiseAndSetIfChanged(ref _symbol, value); }
        }
        public uint Order
        {
            get { return _order; }
            set { this.RaiseAndSetIfChanged(ref _order, value); }
        }
        public OperatorArithmeticType ArithmeticType
        {
            get { return _arithmeticType; }
            set { this.RaiseAndSetIfChanged(ref _arithmeticType, value); }
        }
        public OperatorFunction Function
        {
            get { return _function; }
            set { this.RaiseAndSetIfChanged(ref _function, value); }
        }
    }
}
