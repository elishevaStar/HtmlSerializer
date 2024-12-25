using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        // Static function to parse a selector string
        public static Selector Parse(string query)
        {
            var parts = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Selector root = null;
            Selector current = null;

            foreach (var part in parts)
            {
                var selector = new Selector();

                // Parse tag name, ID, and classes
                var tagMatch = Regex.Match(part, @"^([a-zA-Z][a-zA-Z0-9]*)");
                if (tagMatch.Success) selector.TagName = tagMatch.Groups[1].Value;

                var idMatch = Regex.Match(part, @"#([a-zA-Z0-9-_]+)");
                if (idMatch.Success) selector.Id = idMatch.Groups[1].Value;

                var classMatches = Regex.Matches(part, @"\.([a-zA-Z0-9-_]+)");
                foreach (Match match in classMatches)
                {
                    selector.Classes.Add(match.Groups[1].Value);
                }

                // Link selectors in hierarchy
                if (current != null)
                {
                    current.Child = selector;
                    selector.Parent = current;
                }

                current = selector;
                if (root == null) root = selector;
            }

            return root;
        }
    }
}
