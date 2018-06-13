// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class CreateWorkOrder
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void UCITestCreateWorkOrder()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);

                xrmApp.Navigation.OpenApp("Field Service");

                xrmApp.Navigation.OpenSubArea("Field Service", "Work Orders");

                xrmApp.CommandBar.ClickCommand("New");

                xrmApp.Entity.SetValue(new LookupItem { Name = "msdyn_serviceaccount", Value = "Acc" });
                xrmApp.ThinkTime(1000);
                xrmApp.Entity.SetValue(new LookupItem { Name = "msdyn_workordertype", Value = "WO type" });
                xrmApp.ThinkTime(1000);
                xrmApp.Entity.SetValue(new LookupItem { Name = "msdyn_pricelist", Value = "pl list" });
                xrmApp.ThinkTime(1000);

                xrmApp.Entity.Save();
            }
        }
    }
}
