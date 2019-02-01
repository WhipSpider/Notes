using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Notes
{
    class Notes_Service
    {
        public void SaveList(ObservableCollection<Note> _list)
        {
            FileStream stream1 = new FileStream("Notes.json", FileMode.Create);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Note>));
            ser.WriteObject(stream1, _list);
            stream1.Close();
        }

        public ObservableCollection<Note> LoadList()
        {
            ObservableCollection<Note> _list = new ObservableCollection<Note>();
            FileStream stream1 = new FileStream("Notes.json",  FileMode.OpenOrCreate, FileAccess.Read);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Note>));
            _list = (ObservableCollection<Note>)ser.ReadObject(stream1);
            stream1.Close();
            return _list;
        }

    }
}
