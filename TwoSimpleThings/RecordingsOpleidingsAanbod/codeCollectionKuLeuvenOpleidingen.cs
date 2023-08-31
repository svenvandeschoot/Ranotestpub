/*
 * Created by Ranorex
 * User: u0143405
 * Date: 10/07/2023
 * Time: 10:28
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace TwoSimpleThings.RecordingsOpleidingsAanbod
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class codeCollectionKuLeuvenOpleidingen
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        /// 
        
        private static TwoSimpleThingsRepository r = TwoSimpleThingsRepository.Instance;
        private static bool verderGaan;

        
        private static string kulLogo = "https://stijl.kuleuven.be/releases/latest/img/svg/logo.svg";
        
        [UserCodeMethod]
        public static void openWebsite(string teOpenenSite, string teStartenBrowser)
        {
        	Report.Log(ReportLevel.Info, "De website: '"+teOpenenSite+"' openen in browser: '"+teStartenBrowser+"'.");
        	try {
        		Host.Current.OpenBrowser(teOpenenSite, teStartenBrowser, "", false, true, false, false, false, false, false, false);
				verderGaan = true;        		
        	} catch (Exception) {
        		verderGaan = false;
        		throw new Exception("De gevraagde website kon niet worden geopend.");
        	}
        }
        [UserCodeMethod]
        public static void controleerWebsiteGeopend()
        {
        	Report.Log(ReportLevel.Info, "Controleer geopende website.");
        	r.KULeuven.Self.EnsureVisible();
        	if (verderGaan) {
        		Validate.IsTrue(r.KULeuven.SelfInfo.Exists(), "De website werd correct geopend.");
        	} else {Report.Log(ReportLevel.Warn, "Er werd geen website geopend en er kan niets gevalideerd worden.");}
        		

        }
        
        [UserCodeMethod]
        public static void controleerWebsiteLogo()
        {
        	Report.Log(ReportLevel.Info, "Controleer website logo.");
        	r.KULeuven.Self.EnsureVisible();
        	if (verderGaan) {
        		string logo = r.KULeuven.Logo.Element.GetAttributeValueText("src");
        		
        		Validate.IsTrue(logo.Equals(kulLogo), "Het verwachte logo wordt getoond op de website");
        	} else { Report.Log(ReportLevel.Warn, "De website werd niet geopend en kan niet worden gecontroleerd."); }
        }
        
        [UserCodeMethod]
        public static void sluitBrowser()
        {
        	Report.Log(ReportLevel.Info, "Browser sessie sluiten.");
        	
        	r.KULeuven.SelfInfo.FindAdapter<WebDocument>().Close();
        }
        
        

        

        
    }
}
