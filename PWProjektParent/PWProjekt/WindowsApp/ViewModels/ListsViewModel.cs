using Milek_Nowak_Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;


namespace Milek_Nowak_WindowsApp.ViewModels
{
    public class ListsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView _gamesView;
        private ListCollectionView _producersView;

        private IDAO _dao;

        private ObservableCollection<GameViewModel> _gamesVm;
        public ObservableCollection<GameViewModel> GamesVm
        {
            get { return _gamesVm; }
            set
            {
                _gamesVm = value;
                RaisePropertyChanged(nameof(GamesVm));
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

        #region gameList

        private string _gamesFilter;
        public string GamesFilter
        {
            get => _gamesFilter;
            set
            {
                _gamesFilter = value;
                RaisePropertyChanged(nameof(GamesFilter));
            }
        }

        private GameViewModel _selectedGame;
        public GameViewModel SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                if (CanAddNewGame())
                {
                    EditedGame = SelectedGame;
                }                
                RaisePropertyChanged(nameof(SelectedGame));

            }
        }

        private GameViewModel _editedGame;
        public GameViewModel EditedGame
        {
            get => _editedGame;
            set
            {
                _editedGame = value;
                RaisePropertyChanged(nameof(EditedGame));
            }
        }

        private void AddNewGame()
        {
            IGame newGame = _dao.CreateNewGame();
            GameViewModel pvm = new GameViewModel(newGame);
            EditedGame = pvm;
            EditedGame.IsChanged = true;
            SelectedGame = null;
        }
        private bool CanAddNewGame()
        {
            if ((EditedGame == null) || (!EditedGame.IsChanged)) 
            { 
                return true;
            }
            
            return false;
        }
        private void SaveGame()
        {
            if (EditedGame.HasErrors)
            {
                return;
            }
            if (EditedGame.Id == 0)
            {
                GamesVm.Add(EditedGame);
                _dao.AddGame(EditedGame.Game);
            }
            EditedGame.IsChanged = false;
            _dao.SaveChanges();
            EditedGame = null;
        }
        private bool CanSaveGame()
        {
            if ((EditedGame == null) || !EditedGame.IsChanged)
            {
                return false;
            }
            return !EditedGame.HasErrors;
        }
        private void DeleteGame()
        {
            _dao.RemoveGame(EditedGame.Game);
            _dao.SaveChanges();

            GamesVm.Remove(EditedGame);

            SelectedGame = null;
            EditedGame = null;
        }
        private bool CanDeleteGame()
        {
            return EditedGame != null;
        }
        private void UndoGamesChanges()
        {
            if (EditedGame.Id != 0)
            {
                _dao.UndoChanges();
                IGame game = _dao.GetAllGames().First(c => c.Id == EditedGame.Id);
                int index = GamesVm.IndexOf(EditedGame);
                GamesVm[index] = new GameViewModel(game);
            }
            EditedGame = null;
        }
        private bool CanUndoGamesChanges()
        {
            if ((EditedGame == null)) 
            {
                return false; 
            }
            return true;
        }
        private void FilterGamesData()
        {
            if (string.IsNullOrEmpty(_gamesFilter))
            {
                _gamesView.Filter = null;
            }
            else
            {
                _gamesView.Filter = p => ((GameViewModel)p).Name.Contains(_gamesFilter);
            }
        }

        private RelayCommand _addNewGameCommand;
        public RelayCommand AddNewGameCommand
        {
            get => _addNewGameCommand;
        }

        private RelayCommand _saveGameCommand;
        public RelayCommand SaveGameCommand
        {
            get => _saveGameCommand;
        }

        private RelayCommand _deleteGameCommand;
        public RelayCommand DeleteGameCommand
        {
            get => _deleteGameCommand;
        }

        private RelayCommand _filterGamesDataCommand;
        public RelayCommand FilterGamesDataCommand
        {
            get => _filterGamesDataCommand;
        }

        private RelayCommand _undoGamesChangesCommand;
        public RelayCommand UndoGamesChangesCommand
        {
            get => _undoGamesChangesCommand;
        }

        #endregion



        public ListsViewModel()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            _dao = Milek_Nowak_BLC.BLC.GetInstance(libraryName).DAO;

            _producersVm = new ObservableCollection<ProducerViewModel>();
            _gamesVm = new ObservableCollection<GameViewModel>();

            _producers = new ObservableCollection<IProducer>(_dao.GetAllProducers());

            foreach (var producer in _dao.GetAllProducers())
            {
                ProducersVm.Add(new ProducerViewModel(producer));
            }

            foreach (var game in _dao.GetAllGames())
            {
                GamesVm.Add(new GameViewModel(game));
            }

            _producersView = (ListCollectionView)CollectionViewSource.GetDefaultView(_producersVm);
            _addNewProducerCommand = new RelayCommand(param => AddNewProducer(), _ => CanAddNewProducer());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), _ => CanSaveProducer());
            _deleteProducerCommand = new RelayCommand(_ => DeleteProducer(), _ => CanDeleteProducer());
            _filterProducersDataCommand = new RelayCommand(param => FilterProducersData());
            _undoProducersChangesCommand = new RelayCommand(param => UndoProducersChanges(), _ => CanUndoProducersChanges());


            _gamesView = (ListCollectionView)CollectionViewSource.GetDefaultView(_gamesVm);
            _addNewGameCommand = new RelayCommand(_ => AddNewGame(), _ => CanAddNewGame());
            _saveGameCommand = new RelayCommand(_ => SaveGame(), _ => CanSaveGame());
            _deleteGameCommand = new RelayCommand(_ => DeleteGame(), _ => CanDeleteGame());
            _filterGamesDataCommand = new RelayCommand(param => FilterGamesData());
            _undoGamesChangesCommand = new RelayCommand(param => UndoGamesChanges(), _ => CanUndoGamesChanges());
        }
    }
}
