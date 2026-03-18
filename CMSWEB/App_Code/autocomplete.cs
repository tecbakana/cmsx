using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using CMSXBLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

/// <summary>
/// Summary description for autocomplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class autocomplete : BaseWS {

    public autocomplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public void GetCompletionList(string prefixText, int count)
    {
        _obj.parms = 1;
        _obj.urlcliente = prefixText;
        _apprepo.MakeConnection(_obj);

        //var apps = _apprepo.ListaApp();// _apprepo.ListaApp();

        //return JsonConvert.SerializeObject(apps);
    
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetExistent(string prefixText, int count)
    {
        _obj.parms = 1;
        _obj.urlcliente = prefixText;
        _apprepo.MakeConnection(_obj);

        var p = new retJson();
        /*var ret = _apprepo.ListaApp();
        p.count = ret.Count();
        p.valid=true;

        string jsonformatstring = JsonConvert.SerializeObject(p, Formatting.Indented);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        HttpContext.Current.Response.Write(jsonformatstring);
        HttpContext.Current.Response.End();*/

        //return;
    }
}
