
using System;

namespace GS.UI
{
    internal enum DataType
    {
        Integer,
        Float,
        String
    }

    [Serializable]
    internal struct TD_DataLabel
    {
        public string dataTypeCode;

        public DataType dataType;
    }
}
