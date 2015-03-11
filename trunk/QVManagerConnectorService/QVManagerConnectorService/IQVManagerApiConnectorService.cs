using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace QVManagerConnectorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQVManagerApiConnectorService" in both code and config file together.
    [ServiceContract]
    public interface IQVManagerApiConnectorService
    {
        [OperationContract]
        [WebGet]
        System.IO.Stream ListarCALRaw();

        [OperationContract]
        [WebGet]
        System.IO.Stream ListarDCALRaw();
    }
}
