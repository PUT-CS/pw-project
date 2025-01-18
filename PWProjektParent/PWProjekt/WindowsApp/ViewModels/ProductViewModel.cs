using Milek_Nowak_Core;
using Milek_Nowak_Interfaces;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Milek_Nowak_WindowsApp.ViewModels
{
    public class BroomViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private IBroom _broom;
        public IBroom Broom => _broom;

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
            get => _broom.Id;
            set
            {
                _broom.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(50, ErrorMessage = "Nazwa nie może być dłuższa niż 50 znaków")]
        public string Name
        {
            get => _broom.Name;
            set
            {
                IsChanged = true;
                _broom.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być liczbą dodatnią.")]
        public double Price
        {
            get => _broom.Price;
            set
            {
                IsChanged = true;
                _broom.Price = Math.Round(value,2);
                RaisePropertyChanged(nameof(Price));
            }
        }
        [Required(ErrorMessage = "Producent jest wymagany")]
        public IProducer Producer
        {
            get => _broom.Producer;
            set
            {
                IsChanged = true;
                _broom.Producer = value;
                RaisePropertyChanged(nameof(Producer));
            }
        }

        [Required(ErrorMessage = "Materiał trzonka jest wymagany")]
        public GameTheme HandleMaterial
        {
            get => _broom.HandleMaterial;
            set
            {
                IsChanged = true;
                _broom.HandleMaterial = value;
                RaisePropertyChanged(nameof(HandleMaterial));
            }
        }
        [Required(ErrorMessage = "Materiał włosia jest wymagany")]
        public GameType FibersMaterial
        {
            get => _broom.FibersMaterial;
            set
            {
                IsChanged = true;
                _broom.FibersMaterial = value;
                RaisePropertyChanged(nameof(FibersMaterial));
            }
        }

        public BroomViewModel(IBroom broom)
        {
            _broom = broom;
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
