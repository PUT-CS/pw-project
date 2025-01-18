using Barski_Lewandowski_Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;


namespace Barski_Lewandowski_WindowsApp.ViewModels
{
    public class ListsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView _broomsView;
        private ListCollectionView _producersView;

        private IDAO _dao;

        private ObservableCollection<BroomViewModel> _broomsVm;
        public ObservableCollection<BroomViewModel> BroomsVm
        {
            get { return _broomsVm; }
            set
            {
                _broomsVm = value;
                RaisePropertyChanged(nameof(BroomsVm));
            }
        }

        private ObservableCollection<ProducerViewModel> _producersVm;
        public ObservableCollection<ProducerViewModel> ProducersVm
        {
            get { return _producersVm; }
            set
            {
                _producersVm = value;
                RaisePropertyChanged(nameof(ProducersVm));
            }
        }

        private ObservableCollection<IProducer> _producers;
        public ObservableCollection<IProducer> Producers
        {
            get { return _producers; }
            set
            {
                _producers = value;
                RaisePropertyChanged(nameof(Producers));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }            
        }

        #region producerList

        private string _producersFilter;
        public string ProducersFilter
        {
            get => _producersFilter;
            set
            {
                _producersFilter = value;
                RaisePropertyChanged(nameof(ProducersFilter));
            }
        }

        private ProducerViewModel _selectedProducer;
        public ProducerViewModel SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                if (CanAddNewProducer())
                {
                    EditedProducer = SelectedProducer;
                }
                    
                RaisePropertyChanged(nameof(SelectedProducer));

            }
        }

        private ProducerViewModel _editedProducer;
        public ProducerViewModel EditedProducer
        {
            get => _editedProducer;
            set
            {
                _editedProducer = value;
                RaisePropertyChanged(nameof(EditedProducer));
            }
        }


        private void AddNewProducer()
        {
            IProducer newProducer = _dao.CreateNewProducer();
            ProducerViewModel pvm = new ProducerViewModel(newProducer);
            EditedProducer = pvm;
            EditedProducer.IsChanged = true;
            SelectedProducer = null;
        }
        private bool CanAddNewProducer()
        {
            if ((EditedProducer == null) || (!EditedProducer.IsChanged)) 
            {
                return true;
            }
            return false;
        }
        private void SaveProducer()
        {
            if (EditedProducer.HasErrors)
            {
                return;
            }
            if (EditedProducer.Id == 0)
            {
                ProducersVm.Add(EditedProducer);
                Producers.Add(EditedProducer.Producer);
                _dao.AddProducer(EditedProducer.Producer);
            }
            EditedProducer.IsChanged = false;
            _dao.SaveChanges();
            EditedProducer = null;
        }
        private bool CanSaveProducer()
        {
            if ((EditedProducer == null) || !EditedProducer.IsChanged)
            {
                return false;
            }
            return !EditedProducer.HasErrors;
        }
        private void DeleteProducer()
        {
            _dao.RemoveProducer(EditedProducer.Producer);
            _dao.SaveChanges();

            ProducersVm.Remove(EditedProducer);
            Producers.Remove(EditedProducer.Producer);

            SelectedProducer = null;
            EditedProducer = null;
        }
        private bool CanDeleteProducer()
        {
            return EditedProducer != null;
        }
        private void UndoProducersChanges()
        {
            if (EditedProducer.Id != 0)
            {
                _dao.UndoChanges();
                IProducer producer = _dao.GetAllProducers().First(c => c.Id == EditedProducer.Id);
                int index = ProducersVm.IndexOf(EditedProducer);
                ProducersVm[index] = new ProducerViewModel(producer);
            }
            EditedProducer = null;
        }
        private bool CanUndoProducersChanges()
        {
            if ((EditedProducer == null))
            {
                return false;
            }
            return true;
        }
        private void FilterProducersData()
        {
            if (string.IsNullOrEmpty(_producersFilter))
            {
                _producersView.Filter = null;
            }
            else
            {
                _producersView.Filter = p => ((ProducerViewModel)p).Name.Contains(_producersFilter);
            }
        }


        private RelayCommand _addNewProducerCommand;
        public RelayCommand AddNewProducerCommand
        {
            get => _addNewProducerCommand;
        }

        private RelayCommand _saveProducerCommand;
        public RelayCommand SaveProducerCommand
        {
            get => _saveProducerCommand;
        }

        private RelayCommand _deleteProducerCommand;
        public RelayCommand DeleteProducerCommand
        {
            get => _deleteProducerCommand;
        }

        private RelayCommand _filterProducersDataCommand;
        public RelayCommand FilterProducersDataCommand
        {
            get => _filterProducersDataCommand;
        }

        private RelayCommand _undoProducersChangesCommand;
        public RelayCommand UndoProducersChangesCommand
        {
            get => _undoProducersChangesCommand;
        }

        #endregion

        #region broomList

        private string _broomsFilter;
        public string BroomsFilter
        {
            get => _broomsFilter;
            set
            {
                _broomsFilter = value;
                RaisePropertyChanged(nameof(BroomsFilter));
            }
        }

        private BroomViewModel _selectedBroom;
        public BroomViewModel SelectedBroom
        {
            get => _selectedBroom;
            set
            {
                _selectedBroom = value;
                if (CanAddNewBroom())
                {
                    EditedBroom = SelectedBroom;
                }                
                RaisePropertyChanged(nameof(SelectedBroom));

            }
        }

        private BroomViewModel _editedBroom;
        public BroomViewModel EditedBroom
        {
            get => _editedBroom;
            set
            {
                _editedBroom = value;
                RaisePropertyChanged(nameof(EditedBroom));
            }
        }

        private void AddNewBroom()
        {
            IBroom newBroom = _dao.CreateNewBroom();
            BroomViewModel pvm = new BroomViewModel(newBroom);
            EditedBroom = pvm;
            EditedBroom.IsChanged = true;
            SelectedBroom = null;
        }
        private bool CanAddNewBroom()
        {
            if ((EditedBroom == null) || (!EditedBroom.IsChanged)) 
            { 
                return true;
            }
            
            return false;
        }
        private void SaveBroom()
        {
            if (EditedBroom.HasErrors)
            {
                return;
            }
            if (EditedBroom.Id == 0)
            {
                BroomsVm.Add(EditedBroom);
                _dao.AddBroom(EditedBroom.Broom);
            }
            EditedBroom.IsChanged = false;
            _dao.SaveChanges();
            EditedBroom = null;
        }
        private bool CanSaveBroom()
        {
            if ((EditedBroom == null) || !EditedBroom.IsChanged)
            {
                return false;
            }
            return !EditedBroom.HasErrors;
        }
        private void DeleteBroom()
        {
            _dao.RemoveBroom(EditedBroom.Broom);
            _dao.SaveChanges();

            BroomsVm.Remove(EditedBroom);

            SelectedBroom = null;
            EditedBroom = null;
        }
        private bool CanDeleteBroom()
        {
            return EditedBroom != null;
        }
        private void UndoBroomsChanges()
        {
            if (EditedBroom.Id != 0)
            {
                _dao.UndoChanges();
                IBroom broom = _dao.GetAllBrooms().First(c => c.Id == EditedBroom.Id);
                int index = BroomsVm.IndexOf(EditedBroom);
                BroomsVm[index] = new BroomViewModel(broom);
            }
            EditedBroom = null;
        }
        private bool CanUndoBroomsChanges()
        {
            if ((EditedBroom == null)) 
            {
                return false; 
            }
            return true;
        }
        private void FilterBroomsData()
        {
            if (string.IsNullOrEmpty(_broomsFilter))
            {
                _broomsView.Filter = null;
            }
            else
            {
                _broomsView.Filter = p => ((BroomViewModel)p).Name.Contains(_broomsFilter);
            }
        }

        private RelayCommand _addNewBroomCommand;
        public RelayCommand AddNewBroomCommand
        {
            get => _addNewBroomCommand;
        }

        private RelayCommand _saveBroomCommand;
        public RelayCommand SaveBroomCommand
        {
            get => _saveBroomCommand;
        }

        private RelayCommand _deleteBroomCommand;
        public RelayCommand DeleteBroomCommand
        {
            get => _deleteBroomCommand;
        }

        private RelayCommand _filterBroomsDataCommand;
        public RelayCommand FilterBroomsDataCommand
        {
            get => _filterBroomsDataCommand;
        }

        private RelayCommand _undoBroomsChangesCommand;
        public RelayCommand UndoBroomsChangesCommand
        {
            get => _undoBroomsChangesCommand;
        }

        #endregion



        public ListsViewModel()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            _dao = Barski_Lewandowski_BLC.BLC.GetInstance(libraryName).DAO;

            _producersVm = new ObservableCollection<ProducerViewModel>();
            _broomsVm = new ObservableCollection<BroomViewModel>();

            _producers = new ObservableCollection<IProducer>(_dao.GetAllProducers());

            foreach (var producer in _dao.GetAllProducers())
            {
                ProducersVm.Add(new ProducerViewModel(producer));
            }

            foreach (var broom in _dao.GetAllBrooms())
            {
                BroomsVm.Add(new BroomViewModel(broom));
            }

            _producersView = (ListCollectionView)CollectionViewSource.GetDefaultView(_producersVm);
            _addNewProducerCommand = new RelayCommand(param => AddNewProducer(), _ => CanAddNewProducer());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), _ => CanSaveProducer());
            _deleteProducerCommand = new RelayCommand(_ => DeleteProducer(), _ => CanDeleteProducer());
            _filterProducersDataCommand = new RelayCommand(param => FilterProducersData());
            _undoProducersChangesCommand = new RelayCommand(param => UndoProducersChanges(), _ => CanUndoProducersChanges());


            _broomsView = (ListCollectionView)CollectionViewSource.GetDefaultView(_broomsVm);
            _addNewBroomCommand = new RelayCommand(_ => AddNewBroom(), _ => CanAddNewBroom());
            _saveBroomCommand = new RelayCommand(_ => SaveBroom(), _ => CanSaveBroom());
            _deleteBroomCommand = new RelayCommand(_ => DeleteBroom(), _ => CanDeleteBroom());
            _filterBroomsDataCommand = new RelayCommand(param => FilterBroomsData());
            _undoBroomsChangesCommand = new RelayCommand(param => UndoBroomsChanges(), _ => CanUndoBroomsChanges());
        }
    }
}
