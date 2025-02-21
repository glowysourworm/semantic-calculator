namespace semantic_calculator.Model
{
    public class LogMessage : ViewModelBase
    {
        string _message;

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }
    }
}
