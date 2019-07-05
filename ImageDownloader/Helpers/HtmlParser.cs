using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ImageDownloader.Helpers
{
    public class HtmlParser
    {
        private string content;
        private const string atributePattern = "{0}=[\"\'](?<{0}>[^\"^\']*)[\"\']";
        private const string tagPattern = @"\<{0}[^\>]*\>";
        public HtmlParser(string inputString)
        {
            content = inputString;
        }
        /// <summary>
        /// Получить значение атрибута
        /// </summary>
        public string getAttributeContent(string atributeName)
        {
            var pattern = string.Format(atributePattern, atributeName);
            var t = Regex.Match(content, pattern);
            return t.Groups[atributeName].Value;
        }
        /// <summary>
        /// Полчить тег
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public HtmlParser GetTag(string tagName)
        {
            var pattern = string.Format(tagPattern, tagName);
            var tag = Regex.Match(content, pattern).Groups[tagName].Value;
            return new HtmlParser(tag);
        }
        /// <summary>
        /// Найти все теги
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public List<HtmlParser> GetTags(string tagName)
        {
            var pattern = string.Format(tagPattern, tagName);
            var tags = Regex.Matches(content, pattern);
            var parsedTags = new List<HtmlParser>();
            foreach (Match tag in tags)
            {
                var t = new HtmlParser(tag.Value);
                parsedTags.Add(t);
            }
            return parsedTags;
        }
    }
}