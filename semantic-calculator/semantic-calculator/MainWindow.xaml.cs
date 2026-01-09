using System.Windows;
using System.Windows.Input;

using semantic_calculator.core.semantic_tree;
using semantic_calculator.core.semantic_tree.Interface;
using semantic_calculator.ViewModel;

namespace semantic_calculator
{
    public partial class MainWindow : Window
    {
        private readonly IStatementFormatter _statementFormatter;
        private readonly IStatementParser _statementParser;
        private readonly ISemanticSymbolTable _symbolTable;

        private MainViewModel _viewModel;
        private SemanticTreeConfiguration _treeConfiguration;

        public MainWindow()
        {
            _treeConfiguration = new SemanticTreeConfiguration();
            _viewModel = new MainViewModel();

            _statementFormatter = new StatementFormatter();
            _statementParser = new StatementParser(_treeConfiguration);
            _symbolTable = new SemanticSymbolTable();

            InitializeComponent();

            // Configure R-Framework:  Create operator list
            var plus = new SimpleOperator("+", 0, OperatorArithmeticType.Addition, OperatorFunction.Arithmetic);
            var minus = new SimpleOperator("-", 1, OperatorArithmeticType.Subtraction, OperatorFunction.Arithmetic);
            var multiplication = new SimpleOperator("*", 2, OperatorArithmeticType.Multiplication, OperatorFunction.Arithmetic);
            var division = new SimpleOperator("/", 3, OperatorArithmeticType.Division, OperatorFunction.Arithmetic);

            _treeConfiguration.AddOperator(plus);
            _treeConfiguration.AddOperator(minus);
            _treeConfiguration.AddOperator(multiplication);
            _treeConfiguration.AddOperator(division);

            // Welcome Messages
            _viewModel.AddCodeLine("Welcome to Semantic-Calculator!");
            _viewModel.AddCodeLine("This application implements scripting language(s) using your own creations!");
            _viewModel.AddCodeLine("Please see Help > Tutorial for more information");

            // Configuration Messages
            _viewModel.AddLog("Loading Configuration...");
            _viewModel.AddLog("");

            foreach (var oper in _treeConfiguration.Operators)
            {
                _viewModel.AddLog("Defining Operator:  " + oper.GetSymbol() + " Type:  " + oper.ArithmeticType);
                _viewModel.AddOperator(oper);
            }

            // THESE WERE NECESSARY:  No explanation from MSFT (yet). Must have been a change to .NET 8.0
            this.OutputLV.Items.Clear();
            this.SidebarLV.Items.Clear();


            this.DataContext = _viewModel;
        }

        private void InputTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var codeLine = this.InputTB.Text;

                if (!_statementFormatter.IsValidPreformat(codeLine))
                {
                    _viewModel.AddCodeLine("Invalid format or syntax");
                }
                else
                {
                    var formattedLine = _statementFormatter.PreFormat(codeLine);
                    var semanticTree = _statementParser.Parse(formattedLine);

                    var result = semanticTree.Execute(_symbolTable);

                    // Error
                    if (result.Status != SemanticTreeResultStatus.Success)
                        _viewModel.AddCodeLine(result.Message, true);

                    else
                    {
                        _viewModel.AddCodeLine(codeLine, false);
                        _viewModel.AddCodeLine(FormatNumericResult(result), false);
                    }

                    this.InputTB.Text = string.Empty;
                }
            }
        }

        private string FormatNumericResult(ISemanticTreeResult result)
        {
            switch (result.NumericType)
            {
                case SemanticTreeNodeEvaluationResult.NumericResultType.Integer:
                    return "= " + result.NumericResult.ToString("N0");
                case SemanticTreeNodeEvaluationResult.NumericResultType.FloatingPoint:
                    return "= " + result.NumericResult.ToString();
                default:
                    throw new Exception("Unhandled numeric result type");
            }
        }
    }
}