using Milek_Nowak_Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milek_Nowak_WindowsApp.ViewModels
{
    public class ProducerViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IProducer _producer;
        public IProducer Producer => _producer;

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
            get => _producer.Id;
            set
            {
                _producer.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może być dłuższa niż 100 znaków")]
        public string Name
        {
            get => _producer.Name;
            set
            {
                IsChanged = true;
                _producer.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        [Required(ErrorMessage = "Kraj jest wymagany")]
        [StringLength(100, ErrorMessage = "Nazwa kraju nie może być dłuższa niż 100 znaków")]
        public string Country
        {
            get => _producer.Country;
            set
            {
                IsChanged = true;
                _producer.Country = value;
                RaisePropertyChanged(nameof(Country));
            }
        }
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"^\+?[0-9\s]*$", ErrorMessage = "Numer telefonu może składać się tylko z cyfr i spacji oraz może zaczynać się od plusa")]
        [StringLength(25, ErrorMessage = "Numer telefonu nie może być dłuższy niż 25 znaków")]
        public string PhoneNumber
        {
            get => _producer.PhoneNumber;
            set
            {
                IsChanged = true;
                _producer.PhoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }

        public ProducerViewModel(IProducer producer)
        {
            _producer = producer;
            _isChanged = false;
        }

        #region INotyifyDataErrorInfo Implementation


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => errorsCollection.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName))
                return null;
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
