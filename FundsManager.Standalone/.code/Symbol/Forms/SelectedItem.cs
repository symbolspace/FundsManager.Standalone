using System;
using System.Runtime.InteropServices;

namespace Symbol.Forms
{
    [Serializable]
    [ComVisible(true)]
    public class SelectedItem<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }

        public SelectedItem(T value)
            : this(value,value == null ? typeof(T).ToString() : value.ToString())
        {

        }
        public SelectedItem(T value, string text)
        {
            Value = value;
            Text = text;
        }
        public virtual TResult Get<TResult>(ValueGetter<TResult> getter)
        {
            if (getter == null)
                throw new ArgumentNullException("getter");
            return getter(Value);
        }

        public delegate TResult ValueGetter<TResult>(T model);
    }
}
