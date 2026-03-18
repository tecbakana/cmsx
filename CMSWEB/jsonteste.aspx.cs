using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

public partial class jsonteste : System.Web.UI.Page
{
    private JObject _job = new JObject();
    private JsonToken _jtk = new JsonToken();

    protected void Page_Load(object sender, EventArgs e)
    {
        btSend.Click+=new EventHandler(btSend_Click);
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
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    protected void btSend_Click(object sender, EventArgs e)
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

        JObject o = JObject.Parse(json);
 
    }


}