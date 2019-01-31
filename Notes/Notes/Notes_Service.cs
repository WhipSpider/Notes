using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Notes
{
    class Notes_Service
    {
        public void SaveList(List<Note> _list)
        {
            FileStream stream1 = new FileStream("Notes.json", FileMode.Create);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Note>));
            ser.WriteObject(stream1, _list);
        }

        public List<Note> LoadList()
        {
            List<Note> _list = new List<Note>();
            FileStream stream1 = new FileStream("Notes.json",  FileMode.OpenOrCreate, FileAccess.Read);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Note>));
            _list = (List<Note>)ser.ReadObject(stream1);
            return _list;
        }

        public void AddToList(List<Note> _list, Note _note )
        {

        }
    }
}
