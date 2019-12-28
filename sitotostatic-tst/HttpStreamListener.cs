using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sitotostatic_tst
{

    class HttpStreamListener : nsIStreamListener
    {
        string buffer;
        public nsIStreamListener originalListener;
        public string fname;
        public string addr;
        string mainpath;
        public List<HttpObserver.bans> datas;
        public HttpStreamListener()
        {
            mainpath = @"C:\mpksite\";
            // this.
            //this.originalListener = null;
            //init buffer here
            //this.receivedData = [];
        }
        public void OnStartRequest(nsIRequest aRequest, nsISupports aContext)
        {
            this.originalListener.OnStartRequest(aRequest, aContext);
            //aRequest.
        }

        public void OnStopRequest(nsIRequest aRequest, nsISupports aContext, int aStatusCode)
        {
            string lpath = mainpath + Uri.UnescapeDataString(addr);
            if (fname.Contains(".php"))
            {
                fname = fname.Replace(".php", ".html");
            }
            if (fname.Contains("?"))
            {
                fname = fname.Replace("?", "");
            }
            System.IO.Directory.CreateDirectory(lpath);

            System.IO.File.WriteAllText(lpath + "/" + fname, buffer, Encoding.Default);
            datas[datas.Count - 1].pathtofile = lpath + "/" + fname;

            foreach (var f1 in datas)
            {
                if (f1.pathtofile != null)
                {
                    string txt = System.IO.File.ReadAllText(f1.pathtofile);
                    foreach (var f2 in datas)
                    {
                        string nurl = "";
                        if (f2.newurl.Contains("index.html"))
                        {
                            nurl = f2.newurl.Replace("index.html", "/");
                        }
                        txt = txt.Replace(f2.oldurl, nurl);
                    }
                    System.IO.File.WriteAllText(f1.pathtofile, txt);
                }


            }


            this.originalListener.OnStopRequest(aRequest, aContext, aStatusCode);
        }

        public void OnDataAvailable(nsIRequest aRequest, nsISupports aContext, nsIInputStream aInputStream, ulong aOffset, uint aCount)
        {

            var binaryInputStream = Xpcom.CreateInstance<nsIBinaryInputStream>("@mozilla.org/binaryinputstream;1");

            var storageStream = Xpcom.CreateInstance<nsIStorageStream>("@mozilla.org/storagestream;1");

            var binaryOutputStream = Xpcom.CreateInstance<nsIBinaryOutputStream>("@mozilla.org/binaryoutputstream;1");

            binaryInputStream.SetInputStream(aInputStream);


            storageStream.Init(8192, aCount);
            binaryOutputStream.SetOutputStream(storageStream.GetOutputStream(0));

            var data = binaryInputStream.ReadBytes(aCount);


            if (data.Length >= aCount)
            {
                buffer += data.Substring(0, (int)aCount);

                binaryOutputStream.WriteBytes(data, aCount);

                this.originalListener.OnDataAvailable(aRequest, aContext, storageStream.NewInputStream(0), aOffset, aCount);
            }

        }
    }
}
