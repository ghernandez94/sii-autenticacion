namespace ammon.sii.autenticacion.excepciones{
    [System.Serializable()]
    public abstract class AutenticacionException : System.Exception{
        public string Estado {get; set;}
        public AutenticacionException() : base("Error en la autenticación.") { }
        public AutenticacionException(string message) : base(message) { }
        public AutenticacionException(string estado, string message) : base(message) { 
            Estado = estado;
        }
        public AutenticacionException(string message, System.Exception inner) : base(message, inner) { }
        protected AutenticacionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}