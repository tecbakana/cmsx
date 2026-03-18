using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using CMSXBLL;
using ICMSX;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

/// <summary>
/// Summary description for json
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    
[ScriptService]
public class json : BaseWS
{

    public json ()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
    public void RetJson()
    {
        string json = @"{
                      ""Name"": ""Apple"",
                      ""Expiry"": new Date(1230422400000),
                      ""Price"": 3.99,
                      ""Sizes"": [
                        ""Small"",
                        ""Medium"",
                        ""Large""
                      ]
                    }";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RetCountries()
    {
        string json = @"{
                      ""Name"": ""Apple"",
                      ""Expiry"": new Date(1230422400000),
                      ""Price"": 3.99,
                      ""Sizes"": [
                        ""Small"",
                        ""Medium"",
                        ""Large""
                      ]
                    }";
    }

    [WebMethod]
    public string HelloWorld()
    {
        return string.Empty;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string AtualizaPosicaoArea(string data)
    {
        string json = data;

        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        string x = data;
        return x;
    }
}

[Serializable]
public class AreaObj
{
    public int posicao { get; set; }
    public Guid areaId { get; set; }
}