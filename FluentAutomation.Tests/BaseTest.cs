﻿using FluentAutomation.Interfaces;

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Tests;
using FluentAutomation.Tests.Pages;
using Xunit;

namespace FluentAutomation.Tests
{
    /// <summary>
    /// Base Test that opens the test to the AUT
    /// </summary>
    public class BaseTest : FluentTest<IWebDriver>
    {
        static BaseTest()
        {
            // Default tests use chrome and load the site
            var phantom = WbTstrWebDriverConfigs.DefaultPhantomJsWebDriverConfig;
            phantom.AddOrSetArgument("--ignore-ssl-errors", true);

            var chrome = WbTstrWebDriverConfigs.DefaultChromeWebDriverConfig;

            var firefox = WbTstrWebDriverConfigs.DefaultFirefoxWebDriverConfig;

            WbTstr.Configure(firefox).Start();
        }

        public BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

            // Create Page Objects
            this.InputsPage = new Pages.InputsPage(this);
            this.AlertsPage = new Pages.AlertsPage(this);
            this.ScrollingPage = new Pages.ScrollingPage(this);
            this.TextPage = new Pages.TextPage(this);
            this.DragPage = new Pages.DragPage(this);
            this.SwitchPage = new Pages.SwitchPage(this);

            FluentSettings.Current.WindowHeight = 1080;
            FluentSettings.Current.WindowWidth = 1920;

            //FluentSession.DisableStickySession();
            I.Open(SiteUrl);
        }

        public string SiteUrl
        {
            get
            {
                return ConfigReader.GetSetting("WebsiteUnderTestBaseUrl");
            }
        }

        public Pages.InputsPage InputsPage = null;
        public Pages.AlertsPage AlertsPage = null;
        public Pages.ScrollingPage ScrollingPage = null;
        public Pages.TextPage TextPage = null;
        public Pages.DragPage DragPage = null;
        public Pages.SwitchPage SwitchPage = null;
    }

    public class AssertBaseTest : BaseTest
    {
        public AssertBaseTest()
            : base()
        {
            Config.OnExpectFailed((ex, state) =>
            {
                // For the purpose of these tests, allow expects to throw (break test)
                throw ex;
            });
        }
    }
}
