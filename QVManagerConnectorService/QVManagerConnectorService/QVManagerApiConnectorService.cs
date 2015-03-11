using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using QVManagerConnectorService.QMSAPIService;
using QVManagerConnectorService.ServiceSupport;
using System.IO;
using System.ServiceModel.Web;

namespace QVManagerConnectorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QVManagerApiConnectorService" in both code and config file together.
    public class QVManagerApiConnectorService : IQVManagerApiConnectorService
    {
        public System.IO.Stream ListarCALRaw()
        {
            QMSClient Client;
            string QMS = ConfigurationManager.AppSettings["QMSUrl"].ToString();
            Client = new QMSClient("BasicHttpBinding_IQMS", QMS);
            string key = Client.GetTimeLimitedServiceKey();
            ServiceKeyClientMessageInspector.ServiceKey = key;

            ServiceInfo[] MyQVS = Client.GetServices(ServiceTypes.QlikViewServer);

            CALConfiguration myCALs = Client.GetCALConfiguration(MyQVS[0].ID, CALConfigurationScope.NamedCALs);
            List<AssignedNamedCAL> currentCALs = myCALs.NamedCALs.AssignedCALs.ToList();

            StringBuilder sb = new StringBuilder();
            string delimiter = "|";

            //Agergar cabecera
            sb.AppendLine("Username" + delimiter + "LastUsed");

            for (int i = 0; i < currentCALs.Count; i++)
            {
                //Console.WriteLine(currentCALs[i].UserName + ", " + currentCALs[i].LastUsed);
                sb.AppendLine(currentCALs[i].UserName + delimiter + currentCALs[i].LastUsed);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return new MemoryStream(Encoding.Default.GetBytes(sb.ToString()));
        }

        public System.IO.Stream ListarDCALRaw()
        {
            QMSClient Client;
            string QMS = ConfigurationManager.AppSettings["QMSUrl"].ToString();
            Client = new QMSClient("BasicHttpBinding_IQMS", QMS);
            string key = Client.GetTimeLimitedServiceKey();
            ServiceKeyClientMessageInspector.ServiceKey = key;

            ServiceInfo[] MyQVS = Client.GetServices(ServiceTypes.QlikViewServer);

            DocumentNode[] allDocs = Client.GetUserDocuments(MyQVS[0].ID);
            DocumentMetaData Meta;

            StringBuilder sb = new StringBuilder();
            string delimiter = "|";

            //Agergar cabecera
            sb.AppendLine("Application" + delimiter + "Username" + delimiter + "LastUsed");

            for (int i = 0; i < allDocs.Length; i++)
            {
                Meta = Client.GetDocumentMetaData(allDocs[i], DocumentMetaDataScope.Licensing);
                List<AssignedNamedCAL> currentCALs = Meta.Licensing.AssignedCALs.ToList();
                for (int a = 0; a < currentCALs.Count; a++)
                {
                    //Console.WriteLine(Meta.UserDocument.Name + ", " + currentCALs[a].UserName + ", " + currentCALs[a].LastUsed);
                    sb.AppendLine(Meta.UserDocument.Name + delimiter + currentCALs[a].UserName + delimiter + currentCALs[a].LastUsed);
                }
            }
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return new MemoryStream(Encoding.Default.GetBytes(sb.ToString()));
        }
    }
}
