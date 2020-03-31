namespace Till.BusinessLogic
{
    [System.Serializable]
    public class TillBLException : System.Exception
    {
        public TillBLException() { }
        public TillBLException(string message) : base(message) { }
        public TillBLException(string message, System.Exception inner) : base(message, inner) { }
        protected TillBLException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}