using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    [Serializable]
    public class SerializableFeedback
    {
        String data;

        public String Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
