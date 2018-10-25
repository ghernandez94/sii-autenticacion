namespace ammon.sii.autenticacion.servicios{
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://DefaultNamespace", ConfigurationName="GetTokenFromSeed")]
    public interface GetTokenFromSeed
    {
        
        [System.ServiceModel.OperationContractAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getStateReturn")]
        System.Threading.Tasks.Task<string> getStateAsync();
        
        [System.ServiceModel.OperationContractAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getVersionMayorReturn")]
        System.Threading.Tasks.Task<string> getVersionMayorAsync();
        
        [System.ServiceModel.OperationContractAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getVersionMenorReturn")]
        System.Threading.Tasks.Task<string> getVersionMenorAsync();
        
        [System.ServiceModel.OperationContractAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getVersionPatchReturn")]
        System.Threading.Tasks.Task<string> getVersionPatchAsync();
        
        [System.ServiceModel.OperationContractAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getTokenReturn")]
        System.Threading.Tasks.Task<string> getTokenAsync(string pszXml);
    }

    public interface GetTokenFromSeedChannel : GetTokenFromSeed, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    public partial class GetTokenFromSeedClient : System.ServiceModel.ClientBase<GetTokenFromSeed>, GetTokenFromSeed
    {       
        public GetTokenFromSeedClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> getStateAsync()
        {
            return base.Channel.getStateAsync();
        }
        
        public System.Threading.Tasks.Task<string> getVersionMayorAsync()
        {
            return base.Channel.getVersionMayorAsync();
        }
        
        public System.Threading.Tasks.Task<string> getVersionMenorAsync()
        {
            return base.Channel.getVersionMenorAsync();
        }
        
        public System.Threading.Tasks.Task<string> getVersionPatchAsync()
        {
            return base.Channel.getVersionPatchAsync();
        }
        
        public System.Threading.Tasks.Task<string> getTokenAsync(string pszXml)
        {
            return base.Channel.getTokenAsync(pszXml);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}