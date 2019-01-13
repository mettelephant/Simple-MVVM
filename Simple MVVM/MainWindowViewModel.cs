using System.ComponentModel;
using System.Windows.Input;

namespace MVVM_Sample
{
    public class MainWindowViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private MyModel _model = new MyModel();

        // This must be public for binding
        // This should be a full property (has backing field so we can have a more complex setter
        // The setter must call the PropertyChangedEventHandler to notify bound objects of change
        public MyModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChangedEvent("Model");
            }
        }

        /// <summary>
        /// Only needs to be gotten.
        /// Must be set in constructor
        /// </summary>
        public ICommand SubmitCmd { get; }

        public MainWindowViewModel()
        {
            SubmitCmd = new DelegateCommand(DoSomethingWithModel);
            Model.Name = "change me!";
        }

        private void DoSomethingWithModel()
        {
            // This is where you would do something with the model
            // Validate its data or something.
            var addToSomewhere = Model.Name;

            // Once done, clear the model for new data entry
            Model = new MyModel();
        }
    }
}
