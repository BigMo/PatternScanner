using PatternScanner.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternScanner.UI
{
    public class ProjectView : TreeView
    {
        private Project project;
        private ImageList images;

        public Project Project
        {
            get { return project; }
            set
            {
                if (project != value)
                {
                    project = value;
                    Nodes.Clear();
                    PopulateView();
                }
            }
        }

        public ProjectView()
        {
            images = new ImageList();
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.ImageSize = new Size(16, 16);
            images.Images.Add(Properties.Resources.ico_project);
            images.Images.Add(Properties.Resources.ico_group);
            images.Images.Add(Properties.Resources.ico_pattern);

            ImageList = images;

            var project = new Project();
            project.Name = "New Project";

            var group = new Group();
            group.Name = "New Group";

            var pattern = Pattern.FromString("E9 ? ? ? ?");
            group.Add(pattern);
            project.Add(group);

            Project = project;
        }

        private void PopulateView()
        {
            var rootNode = Nodes.Add(Project.Name, project.Name, 0);
            rootNode.Tag = Project;
            foreach (var group in Project.Content)
            {
                var groupNode = rootNode.Nodes.Add(group.Name, group.Name, 1);
                groupNode.Tag = group;
                foreach (var pattern in group.Content)
                {
                    var patternNode = groupNode.Nodes.Add(pattern.HybridPattern, pattern.HybridPattern, 2);
                    patternNode.Tag = pattern;
                }
            }
        }
    }
}
