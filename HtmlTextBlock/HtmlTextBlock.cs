﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using HtmlTextBlock;
using System.IO;

#if NETFX_CORE
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
#else
using System.Windows.Controls;
using System.Windows.Documents;
#endif

namespace HtmlTextBlock
{
    public class HtmlTextBlock : TextBlock 
    {
        public static DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string),
                typeof(HtmlTextBlock), new PropertyMetadata("Html", new PropertyChangedCallback(OnHtmlChanged)));

        public string Html { get { return (string)GetValue(HtmlProperty); } set { SetValue(HtmlProperty, value); } }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Parse(Html);
        }

        private void Parse(string html)
        {
            Inlines.Clear();
            if (html == null)
                return;
            HtmlTagTree tree = new HtmlTagTree();
            HtmlParser parser = new HtmlParser(tree); //output
            parser.Parse(new StringReader(html));     //input

            HtmlUpdater updater = new HtmlUpdater(this); //output
            updater.Update(tree);   
        }

        public static void OnHtmlChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            HtmlTextBlock sender = (HtmlTextBlock)s;
            sender.Parse((string)e.NewValue);		
        }

        public HtmlTextBlock()
        {
            Text = "Assign Html Property";
        }

    }
}
