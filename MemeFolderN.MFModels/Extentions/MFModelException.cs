using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Extentions
{
    [Serializable]
    public class MFModelException : Exception
    {
        /// <summary>Свойство со значением ошибки</summary>
        public MFModelExceptionEnum ValueException { get; } = MFModelExceptionEnum.Default;

        #region Конструкторы
        public MFModelException(string message)
            : base(message) { }
        public MFModelException(MFModelExceptionEnum valueException)
            => ValueException = valueException;
        public MFModelException(string message, MFModelExceptionEnum valueException)
            : base(message)
            => ValueException = valueException;
        public MFModelException(string message, Exception innerException)
            : base(message, innerException) { }
        public MFModelException(MFModelExceptionEnum valueException, Exception innerException)
            : base(null, innerException)
            => ValueException = valueException;
        public MFModelException(string message, MFModelExceptionEnum valueException, Exception innerException)
            : base(message, innerException)
            => ValueException = valueException;

        public MFModelException()
        {
        }

        protected MFModelException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        public override string ToString() => ValueException + Environment.NewLine + base.ToString();
    }
}
