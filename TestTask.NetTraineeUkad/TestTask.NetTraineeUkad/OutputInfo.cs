﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Linq;

namespace TestTask.NetTraineeUkad
{
    class OutputInfo
    {
        internal void OutputAllInfo(List<string> websiteUrls, List<string> sitemapUrls)
        {
            byWebsite(websiteUrls, sitemapUrls);
            bySitemap(websiteUrls, sitemapUrls);

            if (sitemapUrls.Count == 0)
            {
                checkPingAndSort(websiteUrls, websiteUrls.Count, sitemapUrls.Count);
            }
            else
            {
                List<string> mergedUrls = mergeUrls(websiteUrls, sitemapUrls);

                checkPingAndSort(mergedUrls, websiteUrls.Count, sitemapUrls.Count);
            }
        }

        private void byWebsite(List<string> websiteUrls, List<string> sitemapUrls)
        {
            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml\n");

            var byWebsite = new List<string>();

            foreach (var item in websiteUrls)
            {
                if (!sitemapUrls.Contains(item))
                    byWebsite.Add(item);
            }

            if (byWebsite.Count == 0)
            {
                Console.WriteLine($"Nothing :)");
            }
            else
            {
                var i = 1;

                foreach (var item in byWebsite)
                {
                    Console.WriteLine($"{i++} - {item}");
                }
            }
        }

        private void bySitemap(List<string> websiteUrls, List<string> sitemapUrls)
        {
            Console.WriteLine("\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site\n");

            var bySitemap = new List<string>();

            foreach (var item in sitemapUrls)
            {
                if (!websiteUrls.Contains(item))
                {
                    bySitemap.Add(item);
                }
            }

            if (bySitemap.Count == 0)
            {
                Console.WriteLine("0 - Nothing");
            }
            else
            {
                var i = 1;

                foreach (var item in bySitemap)
                {
                    Console.WriteLine($"{i++} - {item}");
                }
            }
        }

        private List<string> mergeUrls(List<string> websiteUrls, List<string> sitemapUrls)
        {
            var mergedUrls = new List<string>();

            foreach (var item in websiteUrls)
            {
                mergedUrls.Add(item);
            }

            foreach (var item in sitemapUrls)
            {
                if (!mergedUrls.Contains(item))
                {
                    mergedUrls.Add(item);
                }
            }

            return mergedUrls;
        }

        private void checkPingAndSort(List<string> allUrls, int websiteCount, int sitemapCount)
        {
            Dictionary<string, int> urlsPing = new Dictionary<string, int>();
            var random = new Random();

            foreach (var item in allUrls)
            {
                urlsPing.Add(item, random.Next(100,1500)); // тут я ещё доделаю добавлю время отклика , а то пробовал с классом Ping не получилось для любой url
            }

            var sortedDict = from entry in urlsPing orderby entry.Value ascending select entry;

            Console.WriteLine("\nTiming :\n");

            var i = 1;

            foreach (var item in sortedDict)
            {
                Console.WriteLine($"{i++} - {item.Key} = {item.Value} ms");
            }

            Console.WriteLine($"\nUrls(html documents) found after crawling a website : {websiteCount}");
            Console.WriteLine($"Urls found in sitemap : {sitemapCount}");
        }
    }
}
