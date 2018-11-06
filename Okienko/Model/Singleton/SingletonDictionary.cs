using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Singleton
{
    public sealed class SingletonDictionary
    {
        private static SingletonDictionary m_oInstance = null;
        public static SingletonDictionary Instance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new SingletonDictionary();
                }
                return m_oInstance;
            }
        }

        private Dictionary<string, TypeMetadata> _data = new Dictionary<string, TypeMetadata>();
        private SingletonDictionary()
        {
            Console.WriteLine("Singleton was created");
        }
        public void Add(string name, TypeMetadata type)
        {
            _data.Add(name, type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeMetadata Get(string name)
        {
            TypeMetadata value;
            _data.TryGetValue(name, out value);
            return value;
        }
    }
}
