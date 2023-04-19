using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Application.Interfaces.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerDbManager.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<VerifyCustomerObject> VerifyCustomer(VerifyCustomerObject verifyCustomerObject)
        {
            var _url = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
            var _action = "http://tckimlik.nvi.gov.tr/WS/TCKimlikNoDogrula";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(verifyCustomerObject);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            string soapResult;

            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                XDocument doc = XDocument.Parse(soapResult);

                var unwrappedResponse = doc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

                var unwrappedResponseValue = ((XElement)unwrappedResponse).Value;

                bool myBool;

                if (bool.TryParse(unwrappedResponseValue, out myBool))
                {
                    verifyCustomerObject.Verified = myBool;
                }
                else verifyCustomerObject.Verified = false;

            }

            return Task.FromResult(verifyCustomerObject);
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(VerifyCustomerObject verifyCustomerObject)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
            @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
               xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" 
               xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
        <SOAP-ENV:Body>
            <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                <TCKimlikNo xsi:type=""xsd:long"">" + verifyCustomerObject.TCKN + @"</TCKimlikNo>
                <Ad xsi:type=""xsd:string"">" + verifyCustomerObject.FirstName + @"</Ad>
                <Soyad xsi:type=""xsd:string"">" + verifyCustomerObject.LastName + @"</Soyad>
                <DogumYili xsi:type=""xsd:integer"">" + verifyCustomerObject.BirthDateYear + @"</DogumYili>
            </TCKimlikNoDogrula>
        </SOAP-ENV:Body>
    </SOAP-ENV:Envelope>");
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

    }
}
