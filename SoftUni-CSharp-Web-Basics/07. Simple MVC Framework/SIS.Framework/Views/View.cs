namespace SIS.Framework.Views
{
    using System.Collections.Generic;
    using System.IO;
    using ActionsResults.Contracts;

    public class View : IRenderable
    {
        private readonly string fullyQualifiedTemplateName;

        private View(string fullyQualifiedTemplateName)
        {
            this.fullyQualifiedTemplateName = fullyQualifiedTemplateName;
        }

        public View(string fullyQualifiedTemplateName, Dictionary<string, string> viewBag)
            : this(fullyQualifiedTemplateName)
        {
            this.ViewBag = viewBag;
        }

        private Dictionary<string, string> ViewBag { get; set; }

        private string ReadFile(string fullyQualifiedTemplateName)
        {
            if (!File.Exists(fullyQualifiedTemplateName))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(fullyQualifiedTemplateName);
        }

        public string Render()
        {
            var fullHtml = ReadFile(this.fullyQualifiedTemplateName);

            return InsertViewParameters(fullHtml);
        }

        private string InsertViewParameters(string fileContent)
        {
            foreach (var viewBagKey in ViewBag.Keys)
            {
                var placeHolder = $"{{{{{viewBagKey}}}}}";

                if (fileContent.Contains(viewBagKey))
                {
                    fileContent = fileContent.Replace(placeHolder, this.ViewBag[viewBagKey]);
                }
            }

            return fileContent;
        }
    }
}