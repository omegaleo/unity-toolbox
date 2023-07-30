using System;
using System.Collections.Generic;
using OmegaLeo.Toolbox.Runtime.Extensions;

namespace Omega_Leo_Toolbox.Editor.Models
{
    public class Docs
    {
        public string AssemblyName { get; set; }
        public List<DocClass> Classes { get; set; }
        public List<Docs> Childs { get; set; }

        public Docs(string assemblyName)
        {
            AssemblyName = assemblyName;
            Classes = new List<DocClass>();
            Childs = new List<Docs>();
        }

        public string GenerateMarkdown()
        {
            string markDown = $"# {AssemblyName}{Environment.NewLine}";

            foreach (var c in Classes)
            {
                markDown += c.GenerateMarkdown();
            }
            
            return markDown;
        }
        
        public string GenerateHTML()
        {
            string html = $"<h1>{AssemblyName}</h1>";

            foreach (var c in Classes)
            {
                html += c.GenerateHTML();
            }
            
            return html;
        }
    }

    public class DocClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DocContent> Contents { get; set; }

        public DocClass(string name)
        {
            Name = name;
            Contents = new List<DocContent>();
        }
        
        public string GenerateMarkdown()
        {
            string markDown = $"## {Name}{Environment.NewLine}";
            markDown += $"{Description}{Environment.NewLine}";

            foreach (var content in Contents)
            {
                markDown += content.GenerateMarkdown();
            }
            
            return markDown;
        }
        
        public string GenerateHTML()
        {
            string html = $"<h2>{Name}</h2>";
            html += $"<p>{Description}</p>";

            foreach (var content in Contents)
            {
                html += content.GenerateHtml();
            }
            
            return html;
        }
    }
    
    public class DocContent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DocArgs[] Args { get; set; }

        public DocContent(string name, string description, DocArgs[] args)
        {
            Name = name;
            Description = description;
            Args = args;
        }
        
        public string GenerateMarkdown()
        {
            string markDown = "";

            if (Name.IsNotNullOrEmpty())
            {
                markDown += $"### {Name}{Environment.NewLine}";
            }
            
            markDown += $"{Description}{Environment.NewLine}";

            foreach (var arg in Args)
            {
                markDown += arg.GenerateMarkdown();
            }

            markDown += Environment.NewLine;
            
            return markDown;
        }
        
        public string GenerateHtml()
        {
            string html = "";

            if (Name.IsNotNullOrEmpty())
            {
                html += $"<h3>{Name}</h3>";
            }
            
            html += $"<p>{Description}</p>";

            foreach (var arg in Args)
            {
                html += arg.GenerateHtml();
            }

            html += "<br>";
            
            return html;
        }
    }

    public class DocArgs
    {
        public string Name;
        public string Description;

        public DocArgs(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string GenerateMarkdown()
        {
            string markdown = "";

            if (Name.IsNotNullOrEmpty())
            {
                markdown += $"**{Name}** - ";
            }
            
            markdown += $"{Description}{Environment.NewLine}";

            return markdown;
        }
        
        public string GenerateHtml()
        {
            string markdown = "<p>";

            if (Name.IsNotNullOrEmpty())
            {
                markdown += $"<b>{Name}</b> - ";
            }
            
            markdown += $"{Description}</p>";

            return markdown;
        }
    }
}