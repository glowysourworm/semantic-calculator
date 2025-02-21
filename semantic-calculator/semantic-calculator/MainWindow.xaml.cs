using System.Windows;

using semantic_calculator.core.semantic_tree;
using semantic_calculator.core.semantic_tree.Interface;
using semantic_calculator.Model;

namespace semantic_calculator
{
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new ViewModel();

            // Configure R-Framework:  Create operator list
            var plus = new SimpleOperator("+", OperatorType.Binary);
            var minus = new SimpleOperator("-", OperatorType.Binary);
            var multiplication = new SimpleOperator("*", OperatorType.Binary);
            var division = new SimpleOperator("/", OperatorType.Binary);

            _viewModel.SemanticConfiguration.AddOperator(plus);
            _viewModel.SemanticConfiguration.AddOperator(minus);
            _viewModel.SemanticConfiguration.AddOperator(multiplication);
            _viewModel.SemanticConfiguration.AddOperator(division);

            // Welcome Messages
            _viewModel.AddCodeLine("Welcome to Semantic-Calculator!");
            _viewModel.AddCodeLine("This application implements scripting language(s) using your own creations!");
            _viewModel.AddCodeLine("Please see Help > Tutorial for more information");

            // Configuration Messages
            _viewModel.AddLog("Loading Configuration...");
            _viewModel.AddLog("");

            foreach (var oper in _viewModel.SemanticConfiguration.Operators)
            {
                _viewModel.AddLog("Defining Operator:  " + oper.Syntax + " Type:  " + oper.Type);
            }

            // THESE WERE NECESSARY:  No explanation from MSFT (yet). Must have been a change to .NET 8.0
            this.OutputLV.Items.Clear();
            this.SidebarLV.Items.Clear();


            this.DataContext = _viewModel;
        }
    }
}