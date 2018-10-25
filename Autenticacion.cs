using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using ammon.sii.autenticacion.excepciones;
using ammon.sii.autenticacion.servicios;

namespace ammon.sii.autenticacion{
    public enum Ambiente{
        PRODUCCION,
        CERTIFICACION
    }
    public class Autenticacion{
        private Ambiente _ambiente;
        private CrSeedClient _crSeedClient;
        private GetTokenFromSeedClient _getTokenFromSeedClient;
        private X509Certificate2 _x509Certificate2;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Autenticacion.
        /// </summary>
        /// <param name="ambiente">Se usa para establecer el ambiente del SII.</param>
        /// <param name="x509Certificate2">Se usa para firmar la semilla que provee el SII.</param>
        public Autenticacion(Ambiente ambiente, X509Certificate2 x509Certificate2){
            _ambiente = ambiente;
            _x509Certificate2 = x509Certificate2;
        }

        private System.ServiceModel.Channels.Binding GetDefaultBinding(){
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
            return result;
        }

        private System.ServiceModel.EndpointAddress GetEndpointAddressForSeed(){
            switch(_ambiente){
                case Ambiente.CERTIFICACION:
                    return new System.ServiceModel.EndpointAddress("https://maullin.sii.cl/DTEWS/CrSeed.jws");
                case Ambiente.PRODUCCION:
                    return new System.ServiceModel.EndpointAddress("https://palena.sii.cl/DTEWS/CrSeed.jws");
                default:
                    throw new SeedException();
            }
        }

        private System.ServiceModel.EndpointAddress GetEndpointAddressForToken(){
            switch(_ambiente){
                case Ambiente.CERTIFICACION:
                    return new System.ServiceModel.EndpointAddress("https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws?WSDL");
                case Ambiente.PRODUCCION:
                    return new System.ServiceModel.EndpointAddress("https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws?WSDL");
                default:
                    throw new TokenException();
            }
        }
        private CrSeedClient CrSeedClient {
            get{
                if(_crSeedClient == null){
                    _crSeedClient = new CrSeedClient(GetDefaultBinding(), GetEndpointAddressForSeed());
                }
                return _crSeedClient;
            }
        }

        private GetTokenFromSeedClient GetTokenFromSeedClient {
            get{
                if(_getTokenFromSeedClient == null){
                    _getTokenFromSeedClient = new GetTokenFromSeedClient(GetDefaultBinding(), GetEndpointAddressForToken());
                }
                return _getTokenFromSeedClient;
            }
        }

        /// <summary>
        /// Método asíncrono usado para obtener una Semilla desde el SII.
        /// <para>Una Semilla es un número único y aleatorio que sirve como identificador para la sesión de un cliente y que tiene un timeout de 2 (dos) minutos.</para>
        /// </summary>
        /// <returns>Devuelve una semilla</returns>
        public async Task<string> GetSemillaAsync(){
            try{            
                string strRespuesta = await CrSeedClient.getSeedAsync();

                //Recibe respuesta y extrae Semilla
                XmlDocument xmlRespuesta = new XmlDocument();
                xmlRespuesta.LoadXml(strRespuesta);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlRespuesta.NameTable);
                ns.AddNamespace("SII", xmlRespuesta.DocumentElement.NamespaceURI);

                string strEstado = xmlRespuesta.SelectSingleNode("//ESTADO", ns).InnerText;
                string strSemilla = null;

                if(strEstado.Equals("00"))
                    strSemilla = xmlRespuesta.SelectSingleNode("//SEMILLA", ns).InnerText;
                else{
                    string strGlosa = xmlRespuesta.SelectSingleNode("//GLOSA", ns).InnerText;
                    throw new SeedException(strEstado, string.Format("{1}({0})", strEstado, strGlosa));
                }
                return strSemilla;
            }catch(Exception ex){
                throw new SeedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Método asíncrono usado para obtener un Token desde el SII.
        /// </summary>
        /// <param name="semilla">La semilla se firma y se envía al SII.</param>
        /// <returns>Devuelve un Token</returns>
        public async Task<string> GetTokenAsync(string semilla){
            try{
                //Prepara solicitud
                Utilidades utilidades = new Utilidades();
                XmlDocument solicitudToken = utilidades.CrearSolicitudToken(semilla);
                solicitudToken = utilidades.FirmarXml(_x509Certificate2, solicitudToken, "");
                
                //Recibe respuesta y extrae Token
                string strRespuesta = await GetTokenFromSeedClient.getTokenAsync(solicitudToken.InnerXml);
                XmlDocument xmlRespuesta = new XmlDocument();
                xmlRespuesta.LoadXml(strRespuesta);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlRespuesta.NameTable);
                ns.AddNamespace("SII", xmlRespuesta.DocumentElement.NamespaceURI);

                string strEstado = xmlRespuesta.SelectSingleNode("//ESTADO", ns).InnerText;
                string strToken = null;

                if(strEstado.Equals("00"))
                    strToken = xmlRespuesta.SelectSingleNode("//TOKEN", ns).InnerText;
                else{
                    string strGlosa = xmlRespuesta.SelectSingleNode("//GLOSA", ns).InnerText;
                    throw new TokenException(strEstado, string.Format("{1}({0})", strEstado, strGlosa));
                }
                return strToken;
            }catch(Exception ex){
                throw new TokenException(ex.Message, ex);
            }
        }
    }
}