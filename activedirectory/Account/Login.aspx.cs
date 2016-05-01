using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ActiveDirectory.Models;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.DirectoryServices;


namespace ActiveDirectory.Account
{
    public partial class Login : Page
    {
        protected DirectoryEntry acceso;

        public string Dominio
        {
            get
            {
                return Domain.Text.Trim();
            }
        }
        public string Usuario
        {
            get
            {
                return Username.Text.Trim();
            }
        }
        public string Contrasenia
        {
            get
            {
                return Password.Text.Trim();
            }
        }





        protected void ingresar(object sender, EventArgs e)
        {

            Session["dominio"] = Domain.Text.Trim();
            Session["usuario"] = Username.Text.Trim();
            Session["contrasenia"] = Password.Text.Trim();


            this.acceso = new DirectoryEntry("LDAP://" + Domain.Text.Trim(),
                                                         Username.Text.Trim(),
                                                         Password.Text.Trim(),
                                                         AuthenticationTypes.Secure); //AuthenticationTypes.Secure como 4to parámetro

            try
            {
                DirectorySearcher existe = new DirectorySearcher(acceso);
                SearchResult resultado = existe.FindOne(); //Devuelve el primero que encuentra


                if (resultado != null)
               {
                    lblError.Text = "Usted ha sido autenticado";
                    Server.Transfer("Administrar.aspx", true);
                    //Response.Redirect("Administrar.aspx");
                }


            }
            catch
            {
                lblError.Text = "Error:     Disculpe, usted no ha sido autenticado. \n Por favor, verifique sus datos.";
            }

            
        }

     



    }
}