using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sitotostatic_tst
{
    class HttpObserver : nsIObserver
    {
        private nsIObserverService service;

        public class bans
        {
            public string oldurl;
            public string newurl;
            public string pathtofile;
        }

        private List<bans> datas;


        public HttpObserver()
        {

            service = Xpcom.GetService<nsIObserverService>("@mozilla.org/observer-service;1");
            datas = new List<bans>();
        }

        public void Register()
        {

            service.AddObserver(this, ObserverNotifications.HttpRequests.HttpOnExamineResponse, false);

        }

        public void Unregister()
        {
            //service.RemoveObserver(this, "http-on-modify-request");
        }


        public void Observe(nsISupports aSubject, string aTopic, string aData)
        {


            nsIHttpChannel httpChannel = Xpcom.QueryInterface<nsIHttpChannel>(aSubject);

            if (aTopic == "http-on-examine-response")
            {
                nsACString nsa = new nsACString();
                httpChannel.GetContentTypeAttribute(nsa);
                string url = httpChannel.GetURIAttribute().ToUri().AbsoluteUri;
                if (url.Contains("http://127.0.0.1/wordpress/"))
                {
                    bans bn = new bans();
                    bn.oldurl = url;
                    string fname = url.Replace("http://127.0.0.1/wordpress/", "");


                    if (fname.Length == 0)
                    {
                        fname = "index.html";
                    }
                    if (fname[fname.Length - 1] == '/')
                    {
                        fname = fname + "index.html";
                    }
                    if (fname.Contains("?ver"))
                    {
                        int indexofq = fname.LastIndexOf("?ver");
                        fname = fname.Substring(0, indexofq);
                    }
                    string addr = "";
                    if (fname.Contains("/"))
                    {
                        int indexofbackslash = fname.LastIndexOf("/");
                        addr = fname.Substring(0, indexofbackslash);
                        fname = fname.Substring(indexofbackslash);
                        fname = fname.Replace("/", "");
                    }
                    string bannew = "";
                    if (addr.Length > 0)
                    {
                        bannew = addr + "/" + fname;
                    }
                    else
                    {
                        bannew = fname;
                    }

                    bn.newurl = bannew;
                    datas.Add(bn);
                    nsITraceableChannel TraceablehttpChannel = Xpcom.QueryInterface<nsITraceableChannel>(aSubject);
                    var newListener = new HttpStreamListener();
                    newListener.fname = fname;
                    newListener.addr = addr;
                    newListener.datas = datas;
                    newListener.originalListener = TraceablehttpChannel.SetNewListener(newListener);
                }

            }

        }
    }
}
