
namespace GS.UI
{
    public struct C_DataLabel_Value<T>
        where T : notnull
    {
        public C_DataLabel_Value(
            int a)
        {
            dataValue = default;
        }

        public T DataValue 
        { 
            get
            {
                return dataValue;
            }
            set
            {
                dataValue = value;
            }
        }
        private T dataValue;
    }
}
