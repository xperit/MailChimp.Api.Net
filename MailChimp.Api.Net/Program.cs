﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailChimp.Api.Net.Services.Reports;
using MailChimp.Api.Net.Services.Templates;
using MailChimp.Api.Net.Domain.Reports;
using MailChimp.Api.Net.Services.Campaigns;
using MailChimp.Api.Net.Domain.Campaigns;
using Newtonsoft.Json;
using MailChimp.Api.Net.Domain;
using MailChimp.Api.Net.Domain.Lists.Post;
using System.IO;
using MailChimp.Api.Net.Helper; 

namespace MailChimp.Api.Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MailChimpReports reports = new MailChimpReports();

            try
            {
                //var x = reports.getoverviewbycampaignasync("3709ea682b").result;
                //var x2 = reports.getoverviewbycampaignasync("e6e1eb2be8").result;
                //var x3 = reports.getadviceasync("e6e1eb2be8").result;
                //var x4 = reports.getclickdetailsasync("e6e1eb2be8").result;
                //var x5 = reports.getclickdetailbylinkidasync("e6e1eb2be8", "6defea2fac").result;
                //var x6 = reports.getalllsubscribersinfoasync("e6e1eb2be8", "6defea2fac").result;

                ////subscriber_hash= the md5 hash of the lowercase version of the list member’s email address
                //// var x7 =reports.getspecificsubscriberinfoasync("e6e1eb2be8", "e6e1eb2be8", string subscriber_hash);
                //var x8 = reports.getdomainperformanceasync("e6e1eb2be8").result;
                //var x9 = reports.geteepurlactivityasync("e6e1eb2be8").result;
                //var x10 = reports.getemailactivityasync("e6e1eb2be8").result;
                //// var x11 = reports.getemailactivitybysubscriberasync("e6e1eb2be8",);
                //var x12 = reports.gettoplocationasync("e6e1eb2be8").result;
                //var x13 = reports.getrecipientsinfoasync("e6e1eb2be8").result;
                ////   var x14= reports.getspecificcampaignrecipientasync("e6e1eb2be8");
                //var x15 = reports.getreportforchildcampaignasync("e6e1eb2be8").result;
                //var x16 = reports.getunsubscriberlistasync("e6e1eb2be8").result;
                //  var x17 =getunsubscriberinfoasync("e6e1eb2be8", string subscriber_hash)

                //var k = reports.getoverviewasync().result;
                //var clickdetails = reports.getclickdetailsasync("3709ea682b").result;

                //var x = reports.getoverviewbycampaignasync("3709ea682b").result;
                //var performance = x.timeseries;
                //List<Timesery> listOfPerfmance = performance.ToList<Timesery>();
                //var templates = new MailChimpTemplates();

                //var z = reports.GetOverviewByCampaignAsync("3709ea682b").Result;

                //  var f = templates.GetTemplatesAsync().Result;

                //var k = templates.DeleteATemplateAsync("18085").Result;
                //var kk = templates.GetSpecificTemplateAsync("18085").Result;

                #region CampaignCreation
                MailChimpCampaigns campaign = new MailChimpCampaigns();
                MCCampaignsOverview overview = new MCCampaignsOverview();

                Recipients recipients = new Recipients()
                {
                    list_id = "0a84a63afc"
                };

                Settings campaignSettings = new Settings()
                {
                    subject_line = "Schedule Mail Subject ",
                    title = "Schedule Mail!!! ",
                    from_name = "Shahriar Hossain",
                    reply_to = "shossain@desme.com",
                    template_id = 18073,
                    authenticate = true,
                    auto_footer = false
                };
                Tracking campaignTracking = new Tracking()
                {
                    opens = true,
                    html_clicks = true,
                    text_clicks = true
                };

                ResultWrapper<Campaign> campaignCreationResult = overview.CreateCampaignAsync(Enum.CampaignType.regular, recipients, campaignSettings, campaignTracking).Result;

                if (campaignCreationResult.HasError == false)
                {
                    ContentTemplate template = new ContentTemplate()
                    {
                        id = "18073"
                    };

                    ContentSetting cSetting = new ContentSetting();
                    string path = @"C:\Users\Wahid\Documents\Visual Studio 2012\Projects\MailChimp.Api.Net\MailChimp.Api.Net\EmailTemplates\raw_email_01.txt";
                    FileParser parser = new FileParser();
                    cSetting.html = parser.EmailParser(path);

                    MCCampaignContent campaignContent = new MCCampaignContent();
                    var setContentStatus = campaignContent.SetCampaignContentAsync(campaignCreationResult.Result.id, cSetting).Result;

                    MCCampaignsCheckList mccheckList = new MCCampaignsCheckList();
                    var checkListResult = mccheckList.GetCampaignContentAsync(campaignCreationResult.Result.id).Result;

                    if (checkListResult.is_ready)
                    {
                       // var sendStatus = overview.SendCampaignAsync(campaignCreationResult.Result.id).Result;

                        DateTime dt = new DateTime(2016, 01, 29, 10, 28, 00, DateTimeKind.Utc);

                        var schedule = campaign.ScheduleCampaignAsync(campaignCreationResult.Result.id, dt).Result;
                    }
                }
                else
                {
                    String.Format("Best of Luck :p !");
                }
                #endregion CampaignCreation

                #region Add people to List
                //ListMemberBase ob1 = new ListMemberBase()
                //    {
                //        email_address = "shahriar_cse@desme.com",
                //        email_type = "html",
                //        language = "English",
                //        status = "subscribed"
                //    };

                //ListMemberBase ob2 = new ListMemberBase()
                //{
                //    email_address = "shossain@desme.com",
                //    email_type = "html",
                //    language = "English",
                //    status = "subscribed"
                //};
                //
                #endregion Add people to List

                #region CampaignScheduler
            
                #endregion CampaignScheduler

                Console.Read();
            }
            catch (Exception ex)
            {

                throw ex;
            }



            Console.Read();
        }
    }
}
