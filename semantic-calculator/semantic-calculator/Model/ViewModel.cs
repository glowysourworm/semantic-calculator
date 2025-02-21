using System.Collections.ObjectModel;

using semantic_calculator.core.semantic_tree;

namespace semantic_calculator.Model
{
    public class ViewModel : ViewModelBase
    {
        public SemanticTreeConfiguration SemanticConfiguration { get; private set; }

        public ObservableCollection<LogMessage> LogMessages { get; private set; }

        public ObservableCollection<string> CodeLines { get; private set; }

        public ViewModel()
        {
            this.SemanticConfiguration = new SemanticTreeConfiguration();
            this.LogMessages = new ObservableCollection<LogMessage>();
            this.CodeLines = new ObservableCollection<string>();
        }

        public void AddLog(string message)
        {
            this.LogMessages.Add(new LogMessage()
            {
                Message = message
            });
        }
        public void AddLog(LogMessage message)
        {
            this.LogMessages.Add(message);
        }
        public void AddCodeLine(string line)
        {
            this.CodeLines.Add(line);
        }
    }
}
