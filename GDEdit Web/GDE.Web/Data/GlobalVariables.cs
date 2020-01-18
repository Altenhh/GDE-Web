using System;
using System.Collections.Generic;

namespace GDE.Web.Data
{
    public static class GlobalVariables
    {
        private static LinkItems currentSection = LinkItems.home;
        
        public static LinkItems CurrentSection
        {
            get => currentSection;
            set
            {
                if (Links.ContainsKey(value) || value == LinkItems.error)
                    currentSection = value;
                else
                    throw new Exception($"{value} is not inside the Links Dictionary");
            }
        }

        public static string ProjectName = "GD Web";

        public static Dictionary<LinkItems, string[]> Links = new Dictionary<LinkItems, string[]>
        {
            { 
                LinkItems.home,
                new []
                {
                    "",
                    "news",
                    "team",
                    "changelog",
                    "download",
                    "search"
                }
            },
            {
                LinkItems.levels, 
                new []
                {
                    "subcategory/gdapicategory",
                    "listing",
                    "map packs",
                    "featured",
                }
            },
            {
                LinkItems.rankings,
                new []
                {
                    "",
                    "legacy",
                    "performance",
                    "score",
                    "country"
                }
            },
            {
                LinkItems.community,
                new []
                {
                    "fetchdata",
                    "forums",
                    "contests",
                    "projects"
                }
            },
            {
                LinkItems.help, 
                new []
                {
                    "counter",
                    "wiki",
                    "faq",
                    "rules",
                    "no, really, i need help!"
                }
            }
        };
    }
    
    public enum LinkItems
    {
        home,
        levels,
        community,
        help,
        rankings,
        error
    }
}