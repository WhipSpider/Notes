using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Notes
{
    public class MainVM : INotifyPropertyChanged
    {
        private Note _note;
        private Notes_Service _notes_Service;
       


        public MainVM()
        {
            

            _note = new Note();
            Note note1 = new Note();
            _notelist = new List<Note>();
            _notes_Service = new Notes_Service();
            CreateSaveCommand();
            CreateCancelCommand();
            CreateLoadCommand();
        }
        



        //----------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Note> _notelist;
        public List<Note> NoteList
        {
            get { return _notelist; }
            set
            {
                _notelist.Add(_note);
                OnPropertyChanged("NoteList"); 
}
        }
        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged("Theme"); 
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }

        
        //----------------------------------------------------------------------

        public ICommand SaveCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteSaveCommand()
        {
            return !string.IsNullOrEmpty(Theme);
        }

        private void CreateSaveCommand()
        {
            SaveCommand = new RelayCommand(SaveExecute, CanExecuteSaveCommand);
        }

        public void SaveExecute()
        {
            _note.Theme = Theme;
            _note.Date = Date;
            _note.Text = Text;
            //_notelist.Add(_note);
            _notes_Service.SaveList(_notelist);
            OnPropertyChanged("NoteList");
        }
        //----------------------------------------------------------------------
        public ICommand CreateCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteCreateCommand()
        {
            return !string.IsNullOrEmpty(Theme);
        }

        private void CreateCreateCommand()
        {
            SaveCommand = new RelayCommand(CreateExecute, CanExecuteSaveCommand);
        }

        public void CreateExecute()
        {
            //_note.Theme = Theme;
            //_note.Date = Date;
            //_note.Text = Text;
            ////_notelist.Add(_note);
            //_notes_Service.SaveList(_notelist);
            //OnPropertyChanged("NoteList");
        }

        //----------------------------------------------------------------------

        public ICommand LoadCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteLoadCommand()
        {
            return true;
        }

        private void CreateLoadCommand()
        {
            LoadCommand = new RelayCommand(LoadExecute, CanExecuteLoadCommand);
        }

        public void LoadExecute()
        {
            //_note.LoadNote();
            OnPropertyChanged("Theme");
            OnPropertyChanged("Date");
            OnPropertyChanged("Text");
        }


        //----------------------------------------------------------------------

        public ICommand CancelCommand
        {
            get;
            internal set;
        }

        private void CreateCancelCommand()
        {
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public void CancelExecute()
        {
            Theme = string.Empty;
            Date = string.Empty;
            Text = string.Empty;
        }

    }
    //----------------------------------------------------------------------


    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action methodToExecute;
        private Func<bool> canExecuteEvaluator;
        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke();
        }
    }
}
