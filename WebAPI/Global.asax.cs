using System;
using System.Web.Routing;

#region Aplicacion Global. Punto de inicio.
// Aqui empieza todo !!!
// Se instancia una aplicacion ICar (ICarDMSWebAPIApp)
public class Global : System.Web.HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        //Creamos e iniciamos la applicacion ICarDMSWebAPI       
        var ICarDMSApp = new ICarDMSWebAPI.ICarDMSWebAPIApp();
        ICarDMSApp.Init();
    }

    protected void Session_Start(object sender, EventArgs e)
    {
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    protected void Application_Error(object sender, EventArgs e)
    {
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }

    protected void Application_End(object sender, EventArgs e)
    {
    }
}
#endregion   
 