using Milek_Nowak_Core;
using Milek_Nowak_Interfaces;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Milek_Nowak_WindowsApp.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private IGame _game;
        public IGame Game => _game;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if (propertyName != nameof(HasErrors))
            {
                Validate();
            }
        }

        private bool _isChanged;
        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                RaisePropertyChanged(nameof(IsChanged));
            }
        }

        public int Id
        {
            get => _game.Id;
            set
            {
                _game.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name
        {
            get => _game.Name;
            set
            {
                IsChanged = true;
                _game.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price
        {
            get => _game.Price;
            set
            {
                IsChanged = true;
                _game.Price = Math.Round(value,2);
                RaisePropertyChanged(nameof(Price));
            }
        }
        [Required(ErrorMessage = "Producer is required")]
        public IProducer Producer
        {
            get => _game.Producer;
            set
            {
                IsChanged = true;
                _game.Producer = value;
                RaisePropertyChanged(nameof(Producer));
            }
        }

        [Required(ErrorMessage = "Game Theme is required")]
        public GameTheme GameTheme
        {
            get => _game.GameTheme;
            set
            {
                IsChanged = true;
                _game.GameTheme = value;
                RaisePropertyChanged(nameof(GameTheme));
            }
        }
        [Required(ErrorMessage = "Game Type is required")]
        public GameType GameType
        {
            get => _game.GameType;
            set
            {
                IsChanged = true;
                _game.GameType = value;
                RaisePropertyChanged(nameof(GameType));
            }
        }

        public GameViewModel(IGame game)
        {
            _game = game;
            _isChanged = false;
        }

        #region INotyifyDataErrorInfo Implementation


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => errorsCollection.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName))
            {
                return null;
            }
            return errorsCollection[propertyName];
        }

        protected void RiseErrorChange(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        private Dictionary<string, ICollection<string>> errorsCollection = new Dictionary<string, ICollection<string>>();

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (var kv in errorsCollection.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RiseErrorChange(kv.Key);

                }
            }

            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;

            foreach (var prop in q)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (errorsCollection.ContainsKey(prop.Key))
                {
                    errorsCollection.Remove(prop.Key);
                }
                errorsCollection.Add(prop.Key, messages);
                RiseErrorChange(prop.Key);
            }
        }
        #endregion
    }
}
