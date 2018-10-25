using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace ammon.sii.autenticacion{
    public class Utilidades{
        public XmlDocument CrearSolicitudToken(string semilla){
            string str = string.Empty;
            str += "<?xml version=\"1.0\"?>";
            str += "<getToken>";
            str += "<item>";
            str += string.Format("<Semilla>{0}</Semilla>", semilla);
            str += "</item>";
            str += "</getToken>";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(str);
            return xmlDocument;
        }
        public XmlDocument FirmarXml(X509Certificate2 x509Certificate2, XmlDocument xmlDocument, string strReference)
        {
            xmlDocument.PreserveWhitespace = true;
            SignedXml signedXml = new SignedXml(xmlDocument);

            signedXml.SigningKey = x509Certificate2.PrivateKey;
            Signature XMLSignature = signedXml.Signature;

            Reference reference = new Reference(strReference);

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            XMLSignature.SignedInfo.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)x509Certificate2.PrivateKey));
            keyInfo.AddClause(new KeyInfoX509Data(x509Certificate2));

            XMLSignature.KeyInfo = keyInfo;

            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(xmlDigitalSignature, true));

            return xmlDocument;            
        }
    }
}