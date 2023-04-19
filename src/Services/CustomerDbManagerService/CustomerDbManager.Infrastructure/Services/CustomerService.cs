using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CustomerDbManager.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<VerifyCustomerObject> VerifyCustomer(VerifyCustomerObject verifyCustomerObject)
        {
            var _url = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
            var _action = "http://tckimlik.nvi.gov.tr/WS/TCKimlikNoDogrula";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
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

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
            @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
               xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" 
               xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
        <SOAP-ENV:Body>
            <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                <TCKimlikNo xsi:type=""xsd:long"">32095302790</TCKimlikNo>
                <Ad xsi:type=""xsd:string"">Hakan</Ad>
                <Soyad xsi:type=""xsd:string"">Çelikten</Soyad>
                <DogumYili xsi:type=""xsd:integer"">1993</DogumYili>
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
