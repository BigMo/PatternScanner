using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.DTO
{
    public abstract class TransparentContainer<T>
    {
        private List<T> content;
        public List<T> Content
        {
            get { return content; }
            set { content = value; }
        }

        public event EventHandler<ElementEventArgs<T>> ElementAdded;
        public event EventHandler<ElementEventArgs<T>> ElementRemoved;

        public class ElementEventArgs<T> : EventArgs
        {
            public T Element { get; private set; }

            public ElementEventArgs(T element)
            {
                Element = element;
            }
        }

        protected TransparentContainer()
        {
            content = new List<T>();
        }

        public void Add(T element)
        {
            if (content.Contains(element))
                throw new Exception($"Container already contains {element}");

            content.Add(element);
            ElementAdded?.Invoke(this, new ElementEventArgs<T>(element));
        }

        public void Remove(T element)
        {
            if (!content.Contains(element))
                throw new Exception($"Container does not contain {element}");

            content.Remove(element);
            ElementRemoved?.Invoke(this, new ElementEventArgs<T>(element));
        }
    }
}
