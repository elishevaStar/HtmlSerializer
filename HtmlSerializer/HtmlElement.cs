using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<String> Classes { get; set; }
        public String InnerHtml { get; set; }

        public HtmlElement Parent { get; set; } // אב של התגית
        public List<HtmlElement> Children { get; set; } // ילדים של התגית

        // בנאי ברירת מחדל
        public HtmlElement()
        {
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        // בנאי עם פרמטרים
        public HtmlElement(string id, string name, string innerHtml = "")
        {
            Id = id;
            Name = name;
            InnerHtml = innerHtml;
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public List<HtmlElement> Query(string selectorString)
        {
            var selector = Selector.Parse(selectorString);
            return QueryBySelector(this, selector);
        }

        private List<HtmlElement> QueryBySelector(HtmlElement element, Selector selector)
        {
            var matches = new List<HtmlElement>();

            foreach (var descendant in element.Descendants())
            {
                if (MatchesSelector(descendant, selector))
                {
                    matches.Add(descendant);
                }
            }

            return matches;
        }

        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            // Match tag name
            if (!string.IsNullOrEmpty(selector.TagName) && element.Name != selector.TagName)
                return false;

            // Match ID
            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
                return false;

            // Match classes
            if (selector.Classes.Any(c => !element.Classes.Contains(c)))
                return false;

            return true;
        }

        //רשימת כל צאצאי האלמנט
        public IEnumerable<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        //רשימת כל אבות האלמנט
        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this.Parent;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }

        








    }
}
