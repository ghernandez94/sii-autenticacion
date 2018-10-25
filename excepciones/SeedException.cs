namespace ammon.sii.autenticacion.excepciones{
    [System.Serializable()]
    public class SeedException : AutenticacionException
    {
        public SeedException() : base("Ocurrió un error desconocido al obtener semilla.") { }
        public SeedException(string estado, string message) : base(estado, message) { }
        public SeedException(string message, System.Exception inner) : base(message, inner) { }
        protected SeedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}