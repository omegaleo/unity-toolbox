using System;
using System.Collections.Generic;
using OmegaLeo.Toolbox.Runtime.Extensions;

namespace Omega_Leo_Toolbox.Editor.Models
{
    public class Docs
    {
        public string AssemblyName { get; set; }
        public List<DocNamespace> Namespaces { get; set; }
        public List<Docs> Childs { get; set; }

        public Docs(string assemblyName)
        {
            AssemblyName = assemblyName;
            Namespaces = new List<DocNamespace>();
            Childs = new List<Docs>();
        }

        public string GenerateMarkdown()
        {
            string markDown = $"# {AssemblyName}{Environment.NewLine}";

            foreach (var n in Namespaces)
            {
                markDown += n.GenerateMarkdown();
            }
            
            return markDown;
        }
        
        public string GenerateHtml()
        {
            var html = "";
            //string html = $"<h1>{AssemblyName}</h1>";

            foreach (var n in Namespaces)
            {
                html += n.GenerateHtml();
            }
            
            return html;
        }

        public string GenerateSidebarHtml()
        {
            var html = "";
            
            foreach (var n in Namespaces)
            {
                html += n.GenerateSidebarHtml();
            }
            
            return html;
        }
    }

    public class DocNamespace
    {
        public string Name;
        public List<DocClass> Classes { get; set; }

        public DocNamespace(string name)
        {
            Name = name;
            Classes = new List<DocClass>();
        }
        
        public string GenerateMarkdown()
        {
            string markDown = $"## {Name}{Environment.NewLine}";

            foreach (var content in Classes)
            {
                markDown += content.GenerateMarkdown();
            }
            
            return markDown;
        }
        
        public string GenerateHtml()
        {
            string html = $"<h2 id='{Name}'>{Name}</h2>";

            foreach (var content in Classes)
            {
                html += content.GenerateHTML();
            }
            
            return html;
        }
        
        public string GenerateSidebarHtml()
        {
            string html = $"<li class='w-100'><a href='#{Name}' class='subLink nav-link px-0'> <span class='d-none d-sm-inline'>{Name}</a></li>";

            foreach (var content in Classes)
            {
                html += content.GenerateSidebarHTML();
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
            string markDown = $"### {Name}{Environment.NewLine}";
            markDown += $"{Description}{Environment.NewLine}";

            foreach (var content in Contents)
            {
                markDown += content.GenerateMarkdown();
            }
            
            return markDown;
        }
        
        public string GenerateHTML()
        {
            string html = $"<h3 id='{Name}'>{Name}</h3>";
            html += $"<p>{Description}</p>";

            foreach (var content in Contents)
            {
                html += content.GenerateHtml();
            }
            
            return html;
        }
        
        public string GenerateSidebarHTML()
        {
            string html = $"<li class='w-100'><a href='#{Name}' class='subSubLink nav-link px-0'> <span class='d-none d-sm-inline'>{Name}</a></li>";

            foreach (var content in Contents)
            {
                html += content.GenerateSidebarHTML();
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
                markDown += $"#### {Name}{Environment.NewLine}";
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
                html += $"<h4 id='{Name}'>{Name}</h4>";
            }
            
            html += $"<p>{Description}</p>";

            foreach (var arg in Args)
            {
                html += arg.GenerateHtml();
            }

            html += "<br>";
            
            return html;
        }
        
        public string GenerateSidebarHTML()
        {
            if (Name.IsNotNullOrEmpty())
            {
                string html = $"<li class='w-100'><a href='#{Name}' class='subSubSubLink nav-link px-0'> <span class='d-none d-sm-inline'>{Name}</a></li>";

                return html;
            }

            return "";
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