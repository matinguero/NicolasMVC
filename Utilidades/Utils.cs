using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProyectoNoticiasNicolas.Utilidades
{
	public class Utils
	{


		#region "Javascripts"
		public static string ReplaceEnters(string str)
		{
			return str.Replace(System.Environment.NewLine, "<br>");
		}


		private static string ReplaceQuotes(string str)
		{
			return str.Replace("'", "''");
		}


		private static string ReplaceJsQuotes(string str)
		{
			return str.Replace("'", "\\'");
		}

		/// <summary>
		/// Muestra un alert de javascript
		/// </summary>
		/// <param name="oPage">El objeto PAGE donde se va a ejecutar el alert</param>
		/// <param name="sMessage">Mensaje a mostrar en el alert</param>
		public static void ShowAlert(Page oPage, string sMessage)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = oPage.GetType();
			ClientScriptManager cs = oPage.ClientScript;

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = "alert('" + SafeJavascript(sMessage) + "');" + System.Environment.NewLine;
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}
		}

		/// <summary>
		/// Muestra un alert de javascript y redirecciona
		/// </summary>
		/// <param name="oPage">El objeto PAGE donde se va a ejecutar el alert</param>
		/// <param name="sMessage">Mensaje a mostrar en el alert</param>
		/// <param name="sUrlRedirect">URL para redirigir</param>
		public static void ShowAlert(Page oPage, string sMessage, string sUrlRedirect)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = oPage.GetType();
			ClientScriptManager cs = oPage.ClientScript;

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = "alert('" + SafeJavascript(sMessage) + "');" + System.Environment.NewLine;
				cstext = cstext + "location.href ='" + sUrlRedirect + "';" + System.Environment.NewLine;
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}
		}

		/// <summary>
		/// Muestra un alert de javascript y redirecciona cuando se está usando AJAX con el ScriptManager
		/// </summary>
		/// <param name="oPage">El objeto PAGE donde se va a ejecutar el alert</param>
		/// <param name="sMessage">Mensaje a mostrar en el alert</param>
		/// <param name="sUrlRedirect">URL para redirigir</param>
		public static void ShowAlertAjax(Page oPage, string sMessage, string sUrlRedirect)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = oPage.GetType();

			string cstext = "alert('" + SafeJavascript(sMessage) + "');" + System.Environment.NewLine;

			if (!string.IsNullOrEmpty(sUrlRedirect.Trim()))
			{
				cstext = cstext + "location.href = '" + sUrlRedirect.Trim() + "';";
			}
			ScriptManager.RegisterClientScriptBlock(oPage, oPage.GetType(), CsName, cstext, true);
		}

		/// <summary>
		/// Crea un confirm de javascript
		/// </summary>
		/// <param name="oBtn">Objeto botón -link button-</param>
		/// <param name="sMessage">Mensaje a mostrar en el confirm</param>
		public static void CrearConfirmLinkButton(ref System.Web.UI.WebControls.LinkButton oBtn, string sMessage)
		{
			oBtn.Attributes.Add("onclick", "return confirm('" + SafeJavascript(sMessage) + "')");
		}

		/// <summary>
		/// Crea un confirm de javascript
		/// </summary>
		/// <param name="oBtn">Objeto botón -command button-</param>
		/// <param name="sMessage">Mensaje a mostrar en el confirm</param>
		public static void CrearConfirmCommandButton(ref System.Web.UI.WebControls.Button oBtn, string sMessage)
		{
			oBtn.Attributes.Add("onclick", "return confirm('" + SafeJavascript(sMessage) + "')");
		}

		/// <summary>
		/// Crea un confirm de javascript
		/// </summary>
		/// <param name="oBtn">Objeto botón -Image button-</param>
		/// <param name="sMessage">Mensaje a mostrar en el confirm</param>
		public static void CrearConfirmImageButton(ref System.Web.UI.WebControls.ImageButton oBtn, string sMessage)
		{
			oBtn.Attributes.Add("onclick", "return confirm('" + SafeJavascript(sMessage) + "')");
		}

		private static string SafeJavascript(string sMessage)
		{
			sMessage = sMessage.Replace(System.Environment.NewLine, "\\n");
			sMessage = sMessage.Replace("'", "\\'");
			return sMessage;
		}

		/// <summary>
		/// Bre ventana por Javascript
		/// </summary>
		/// <param name="sPage">Objeto Page a dónde se va a abrir la ventana</param>
		/// <param name="sPagina">Url de la página a levantar</param>
		/// <param name="sTitle">Título de la página a mostrar</param>
		/// <param name="sParam">Parámetros del window.open</param>
		public static void OpenWindow(Page sPage, string sPagina, string sTitle, string sParam)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = sPage.GetType();
			ClientScriptManager cs = sPage.ClientScript;

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = "window.open('" + sPagina + "','" + sTitle + "','" + sParam + "');";
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}
		}

		/// <summary>
		/// Cierra la ventana abierta con un window.open
		/// </summary>
		/// <param name="sPage">Objeto Page a dónde se va a cerrar la ventana</param>
		/// <param name="bRefreshPadre">Valor booleano que indica si el opener debe refrescarse</param>
		/// <param name="sMessageAlert">Mensaje que quiera mostrarse. Opcional.</param>
		public static void CloseWindow(Page sPage, bool bRefreshPadre, string sMessageAlert)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = sPage.GetType();
			ClientScriptManager cs = sPage.ClientScript;

			System.Text.StringBuilder myscript = new System.Text.StringBuilder("");

			if (!string.IsNullOrEmpty(sMessageAlert))
			{
				myscript.Append("alert('" + SafeJavascript(sMessageAlert) + "');" + System.Environment.NewLine);
			}

			if (bRefreshPadre)
			{
				myscript.Append("opener.location.href=opener.location.href;" + System.Environment.NewLine);
			}
			myscript.Append("window.close();" + System.Environment.NewLine);

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = myscript.ToString();
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}
		}

		/// <summary>
		/// Mueve la página hasta el Anchor indicado
		/// </summary>
		/// <param name="oPage">Objeto Page a dónde se va a ejecutar el movimiento</param>
		/// <param name="sHash">Anchor adónde debe moverse la ventana</param>
		public static void GoToAnchor(Page oPage, string sHash)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = oPage.GetType();
			ClientScriptManager cs = oPage.ClientScript;

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = "function moveWindow() {" + System.Environment.NewLine;
				cstext = cstext + "window.location.hash='" + sHash + "';" + System.Environment.NewLine;
				cstext = cstext + "}";
				cstext = cstext + "moveWindow();";
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}

		}

		/// <summary>
		/// Mueve la página hasta el Anchor indicado si se usa Ajax
		/// </summary>
		/// <param name="sPage">Objeto Page a dónde se va a ejecutar el movimiento</param>
		/// <param name="sAnchor">Anchor adónde debe moverse la ventana</param>
		public static void GoToAnchorAjax(Page sPage, string sAnchor)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = sPage.GetType();

			string cstext = "";
			cstext = cstext + "var obj = document.getElementById('" + sAnchor + "');" + System.Environment.NewLine;
			cstext = cstext + "var d = obj.offsetTop;" + System.Environment.NewLine;
			cstext = cstext + "setTimeout(\"window.scroll(0,d)\",0);" + System.Environment.NewLine;
			ScriptManager.RegisterClientScriptBlock(sPage, sPage.GetType(), CsName, cstext, true);


		}


		public static void GoBack(Page oPage)
		{

			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = oPage.GetType();
			ClientScriptManager cs = oPage.ClientScript;

			if (!cs.IsClientScriptBlockRegistered(CsName))
			{
				string cstext = "window.history.back();" + System.Environment.NewLine;
				cs.RegisterStartupScript(cstype, CsName, cstext, true);
			}




		}


		public static void MostrarLoadingAjax(Page sPage, UpdateProgress cUdpdatePanel)
		{
			string CsName = System.Guid.NewGuid().ToString();
			Type cstype = sPage.GetType();

			string cstext = "";
			cstext = cstext + "window.onsubmit = Function() {" + System.Environment.NewLine;
			cstext = cstext + "If(Page_IsValid) {" + System.Environment.NewLine;
			cstext = cstext + "var UpdateProgress = $find(\"" + cUdpdatePanel.ClientID + "\");" + System.Environment.NewLine;
			cstext = cstext + "window.setTimeout(Function() {" + System.Environment.NewLine;
			cstext = cstext + "UpdateProgress.set_visible(True);" + System.Environment.NewLine;
			cstext = cstext + "}, 100);" + System.Environment.NewLine;
			cstext = cstext + "}" + System.Environment.NewLine;
			cstext = cstext + "}" + System.Environment.NewLine;

			ScriptManager.RegisterClientScriptBlock(sPage, sPage.GetType(), CsName, cstext, true);

		}


		#endregion



		#region "Acceso a páginas del manager"

		public static bool TieneAcceso(string sNombrePagina, int iPerfilId)
		{
			bool bRet = false;

			switch (sNombrePagina)
			{
				//SOLO LOS ADMIN TIENEN ACCESO
				case "USUARIOS":
					if (iPerfilId == 1)
					{
						bRet = true;
					}
					else
					{
						bRet = false;
					}
					break;

				//TODOS TIENEN ACCESO
				case "NOTICIAS":
					bRet = true;
					break;

				default:
					break;
			}

			return bRet;

		}

		#endregion


	}
}