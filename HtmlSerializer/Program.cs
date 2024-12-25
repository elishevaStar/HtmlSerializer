// See https://aka.ms/new-console-template for more information

using HtmlSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = Encoding.UTF8;
var html = await Load("https://learn.malkabruk.co.il/practicode/");

HtmlElement root = BuildHtmlTree(html);

//PrintHtmlTree(root, 0);
var matches = root.Query(".md-grid");

foreach (var match in matches)
{
    Console.WriteLine($"Matched Element: <{match.Name} id=\"{match.Id}\" class=\"{string.Join(" ", match.Classes)}\">");
}


Console.ReadLine();

static void PrintHtmlTree(HtmlElement element, int indent)
{
    // Print opening tag with indentation
    var openingTag = $"{new string(' ', indent * 2)}<{element.Name}";

    // Add attributes to the opening tag if they exist
    foreach (var attribute in element.Attributes)
    {
        openingTag += $" {attribute.Key}=\"{attribute.Value}\"";
    }

    if (element.Classes.Count > 0)
    {
        openingTag += $" class=\"{string.Join(" ", element.Classes)}\"";
    }

    if (!string.IsNullOrEmpty(element.Id))
    {
        openingTag += $" id=\"{element.Id}\"";
    }

    openingTag += ">";
    Console.WriteLine(openingTag);

    // Print inner HTML if it exists
    if (!string.IsNullOrEmpty(element.InnerHtml))
    {
        Console.WriteLine($"{new string(' ', (indent + 1) * 2)}{element.InnerHtml}");
    }

    // Recursively print children
    foreach (var child in element.Children)
    {
        PrintHtmlTree(child, indent + 1);
    }

    // Print closing tag with indentation
    Console.WriteLine($"{new string(' ', indent * 2)}</{element.Name}>");
}

static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

static HtmlElement BuildHtmlTree(string html)
{
    var htmlLines = new Regex("<.*?>|[^<>]+").Matches(html).Select(m => m.Value);
    var htmlTags = HtmlHelper.Instance.HtmlTags;
    var htmlVoidTags = HtmlHelper.Instance.HtmlVoidTags;

    HtmlElement root = new HtmlElement { Name = "root" };
    HtmlElement currentElement = root;

    foreach (var line in htmlLines)
    {
        var trimmedLine = line.Trim();

        if (string.IsNullOrEmpty(trimmedLine))
            continue;

        if (trimmedLine.StartsWith("</"))
        {
            // Closing tag
            currentElement = currentElement.Parent ?? root;
        }
        else if (trimmedLine.StartsWith("<"))
        {
            // Opening tag or self-closing tag
            var newElement = ParseHtmlElement(trimmedLine, currentElement);

            if (!htmlVoidTags.Contains(newElement.Name))
            {
                currentElement.Children.Add(newElement);
                newElement.Parent = currentElement;
                currentElement = newElement;
            }
            else
            {
                currentElement.Children.Add(newElement);
            }
        }
        else
        {
            // Text content
            currentElement.InnerHtml += trimmedLine;
        }
    }

    return root;
}

static HtmlElement ParseHtmlElement(string line, HtmlElement parent)
{
    var element = new HtmlElement
    {
        Name = new Regex(@"<\s*(\w+)").Match(line).Groups[1].Value,
        Parent = parent
    };

    var attrMatches = new Regex(@"(\w+)=([""'])(.*?)\2").Matches(line);
    foreach (Match match in attrMatches)
    {
        var key = match.Groups[1].Value;
        var value = match.Groups[3].Value;

        if (key == "class")
        {
            element.Classes.AddRange(value.Split(' '));
        }
        else if (key == "id")
        {
            element.Id = value;
        }
        else
        {
            element.Attributes[key] = value;
        }
    }

    return element;
}





