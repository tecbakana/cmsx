using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ICMSX;
using System.IO;


/// <summary>
/// Summary description for utilities
/// </summary>
public class utilities : Iutilities
{
	public utilities()
	{
		
        
	}

    public string retornaIdioma(string name)
    {
        System.Resources.ResourceManager rsx = new System.Resources.ResourceManager(typeof(Resources.lang));
        return rsx.GetString(name, System.Threading.Thread.CurrentThread.CurrentCulture);
    }

    public void CriarArquivo(string fileName,string urlCliente)
    {

        try
        {
            // Check if file already exists. If yes, delete it. 
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }


            string htmlInitial = @"<!DOCTYPE html><html lang='eng'> <head>
                                      <Title>
                                        Flame:IT - Solu&ccedil;&otilde;es Criativas!
                                      </Title>
                                    <style>
                                    html, body, iframe {
                                       margin:0; /* remove any margins of the IFrame and the body tag */
                                       padding:0;
                                       height:100%; /* set height 100% so that it fills the entire view port*/
                                    }
                                    iframe {
                                       display:block; /* force the iframe to display as block */   
                                       height:100%; /* assign 100% height */
                                       width: 100%;
                                       border:none;
                                    }
                                    </style>
                                    </head>
                                    <body>
                                    <iframe src='sitefacil/main?cliente="+ urlCliente +"'  /></body>";

            // Create a new file 
            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file
                Byte[] content = new UTF8Encoding(true).GetBytes(htmlInitial);
                fs.Write(content, 0, content.Length);
            }

        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }  
    }
}

public class retJson
{
    public int count { get; set; }
    public bool valid { get; set; }
}

public class Template
{
    public string tname { get; set; }
    public string turl { get; set; }
}