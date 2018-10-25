namespace ammon.sii.autenticacion.excepciones{    
    [System.Serializable()]
    public class TokenException : AutenticacionException
    {
        public TokenException() : base("Ocurrió un error desconocido al obtener token.") { }
        public TokenException(string estado, string message) : base(estado, message) { }
        public TokenException(string message, System.Exception inner) : base(message, inner) { }
        protected TokenException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}