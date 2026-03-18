<%@ WebHandler Language="C#" Class="sort" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSXBLL.Repositorio;
using CMSXDB;

/// <summary>
/// Summary description for SortProducts
/// </summary>
public class sort : BaseHelper, IHttpHandler
{
    
    private HttpContext httpcont;
    public void ProcessRequest(HttpContext context) 
    {
        var areasId = context.Request["areaOrder"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int ctrl = 0;
        
        foreach(var id in areasId)
        {
            _obj.parms =2;
            _obj.posicao = ctrl;
            _obj.id = id;
            _areasrepo.MakeConnection(_obj);
            _areasrepo.EditaAreaPosicao();
            ctrl++;
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("Success");
    }
     
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}