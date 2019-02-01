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
        public Note Note1
        {
            get { return _note; }
            set
            {
                if (Num != -1)
                {
                    _note = value;
                    Theme = _note.Theme;
                    Date = _note.Date;
                    Text = _note.Text;
                    OnPropertyChanged("Note1");
                }
                else
                {
                    Theme = string.Empty;
                    Date = string.Empty;
                    Text = string.Empty;
                }
            }
        }

        private Notes_Service _notes_Service;
        private int num; //SelectedIndex
        public int Num { get { return num; } set { num = value; OnPropertyChanged("Num"); } }


        public MainVM()
        {
            _note = new Note();
            Num = -1;
            _notelist = new ObservableCollection<Note>();
            _notes_Service = new Notes_Service();
            _notelist = _notes_Service.LoadList();
            OnPropertyChanged("NoteList");
            CreateSaveCommand();
            CreateCancelCommand();
            CreateCreatCommand();
            CreateDelCommand();

        }
        



        //----------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Note> _notelist;
        public ObservableCollection<Note> NoteList
        {
            get { return _notelist;  }
            set
            {
                
                _notelist = value;
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
            return ( Num!=-1);
        }

        private void CreateSaveCommand()
        {
            SaveCommand = new RelayCommand(SaveExecute, CanExecuteSaveCommand);
        }

        public void SaveExecute()
        {
            int NumTemp = Num;
            Note1.Theme = Theme;
            Note1.Date = Date;
            Note1.Text = Text;
            _notes_Service.SaveList(_notelist);
            _notelist = _notes_Service.LoadList();
            OnPropertyChanged("NoteList");
            Num = NumTemp;
        }
        //----------------------------------------------------------------------
        public ICommand CreatCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteCreatCommand()
        {
            return true;
        }

        private void CreateCreatCommand()
        {
            CreatCommand = new RelayCommand(CreatExecute, CanExecuteCreatCommand);
        }

        public void CreatExecute()
        {
            _note = new Note
            {
                Theme = "Theme",
                Date = "Date",
                Text = "Text"
            };
            _notelist.Add(_note);
            
            OnPropertyChanged("NoteList");
        }

        //----------------------------------------------------------------------
        public ICommand DelCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteDelCommand()
        {
            return Num != -1;
        }

        private void CreateDelCommand()
        {
            DelCommand = new RelayCommand(DelExecute, CanExecuteDelCommand);
        }

        public void DelExecute()
        {
            _notelist.RemoveAt(Num);
            _notes_Service.SaveList(_notelist);
            OnPropertyChanged("NoteList");
        }

        //----------------------------------------------------------------------

        public ICommand CancelCommand
        {
            get;
            internal set;
        }
        private bool CanExecuteCancelCommand()
        {
            return Num != -1;
        }
        private void CreateCancelCommand()
        {
            CancelCommand = new RelayCommand(CancelExecute, CanExecuteCancelCommand);
        }

        public void CancelExecute()
        {
            Theme = Note1.Theme;
            Date = Note1.Date;
            Text = Note1.Text;
        }

        

        //----------------------------------------------------------------------
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
