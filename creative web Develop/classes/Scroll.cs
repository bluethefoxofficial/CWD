﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace creative_web_Develop.classes
{
    public partial class Scroll
    {
        private static ScrollViewer FindViewer(DependencyObject root)
        {
            var queue = new Queue<DependencyObject>(new[] { root });

            do
            {
                var item = queue.Dequeue();
                if (item is ScrollViewer) { return (ScrollViewer)item; }
                var count = VisualTreeHelper.GetChildrenCount(item);
                for (var i = 0; i < count; i++) { queue.Enqueue(VisualTreeHelper.GetChild(item, i)); }
            } while (queue.Count > 0);

            return null;
        }

        public static void ToBottom(ListBox listBox)
        {
            var scrollViewer = FindViewer(listBox);

            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += (o, args) =>
                {
                    if (args.ExtentHeightChange > 0) { scrollViewer.ScrollToBottom(); }
                };
            }
        }
    }
}
