using System;
using System.Web.Routing;

#region Aplicacion Global. Punto de inicio.
// Aqui empieza el baile !!!
// Se instancia una aplicacion ICar (ICarDMSApp)
public class Global : System.Web.HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        //Creamos e iniciamos la applicacion ICarDMS       
        var ICarDMSApp = new ICarDMS.App.ICarDMSApp();
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
 