using Gecko;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sitotostatic_tst
{
    public partial class Form1 : Form
    {
        HttpObserver observ;
        List<string> urls;
        public Form1()
        {
            InitializeComponent();

            observ = new HttpObserver();
            observ.Register();
            urls = new List<string>();

        }
        string main = "http://127.0.0.1/wordpress/";
        private void Form1_Load(object sender, EventArgs e)
        {

            ie1.Load += Ie1_Load;
            ie1.ReadyStateChange += Ie1_ReadyStateChange;
            ie1.Navigate(main);

        }

        private void Ie1_ReadyStateChange(object sender, DomEventArgs e)
        {

        }

        private void Ie1_Load(object sender, DomEventArgs e)
        {
            GeckoHtmlElement element = null;
            var geckoDomElement = ie1.Document.DocumentElement;
            if (geckoDomElement is GeckoHtmlElement)
            {
                element = (GeckoHtmlElement)geckoDomElement;
                var OuterHtml = element.OuterHtml;
                var htmlcod = new HtmlAgilityPack.HtmlDocument();
                htmlcod.LoadHtml(OuterHtml);
                var nodes = htmlcod.DocumentNode.SelectNodes("//a");
                foreach (HtmlNode link in nodes)
                {
                    string href = link.GetAttributeValue("href", "").ToString();
                    if (href.Contains(main))
                    {
                        urls.Add(href);
                    }
                    else if (href[0] == '/')
                    {
                        urls.Add(href);
                    }

                    urls = urls.Distinct().ToList();

                }
            }
        }
        int index = 0;
        private void tmgotonext_Tick(object sender, EventArgs e)
        {

            if (ie1.Document.ReadyState == "complete")
            {

                if (index < urls.Count - 1)
                {
                    index++;
                    if (urls[index][0] == '/')
                    {
                        urls[index] = main + urls[index];
                    }
                    ie1.Navigate(urls[index]);

                }
                label1.Text = urls.Count.ToString() + "=" + index;
            }
        }
    }
}
