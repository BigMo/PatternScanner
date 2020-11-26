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
        private ContextMenuStrip ctxProjects;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem ctxProjGroupAdd;
        private ToolStripMenuItem ctxProjGroupRemove;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem ctxProjPatternAdd;
        private ToolStripMenuItem ctxProjPatternRemove;
        private ImageList images;
        private Dictionary<object, TreeNode> nodeMap;

        public enum NodeType { None, Project, Group, Pattern }

        public NodeType SelectedNodeType
        {
            get
            {
                var node = SelectedNode;
                if (node == null || node.Tag == null)
                    return NodeType.None;
                if (node.Tag is Project)
                    return NodeType.Project;
                if (node.Tag is Group)
                    return NodeType.Group;
                if (node.Tag is Pattern)
                    return NodeType.Pattern;
                return NodeType.None;
            }
        }

        public Project SelectedProject => SelectedNode?.Tag as Project;
        public Group SelectedGroup => SelectedNode?.Tag as Group;
        public Pattern SelectedPattern => SelectedNode?.Tag as Pattern;

        public event EventHandler SelectedPatternChanged;

        public Project Project
        {
            get { return project; }
            set
            {
                if (project != value)
                {
                    if (project != null)
                    {
                        project.ElementAdded -= GroupAdded;
                        project.ElementRemoved-= GroupRemoved;
                    }
                    project = value;
                    Nodes.Clear();
                    nodeMap.Clear();

                    if (project != null)
                    {
                        project.ElementAdded += GroupAdded;
                        project.ElementRemoved += GroupRemoved;

                        AddNode(null, project, project.Name, 0);
                        foreach (var g in project.Content)
                            GroupAdded(this, new TransparentContainer<Group>.ElementEventArgs<Group>(g));
                    }                    
                }
            }
        }

        public ProjectView()
        {
            InitializeComponent();

            nodeMap = new Dictionary<object, TreeNode>();

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
            group.Project = project;

            var pattern = Pattern.FromString("E9 ? ? ? ?", "Sample");
            pattern.Group = group;
            group.Add(pattern);
            project.Add(group);

            Project = project;

            AfterLabelEdit += _AfterLabelEdit;
            ctxProjects.Opening += _Opening;

            ctxProjGroupAdd.Click += CtxProjGroupAdd_Click;
            ctxProjPatternAdd.Click += CtxProjPatternAdd_Click;

            ctxProjGroupRemove.Click += (o,e)=> Project.Remove(SelectedGroup);
            ctxProjPatternRemove.Click += (o, e) => SelectedPattern.Group.Remove(SelectedPattern);
            AfterSelect += ProjectView_AfterSelect;
        }

        private void ProjectView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectedNodeType== NodeType.Pattern)
                SelectedPatternChanged?.Invoke(this, EventArgs.Empty);
        }

        private void CtxProjPatternAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPatternImport())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frm.Pattern.Group = SelectedGroup;
                    SelectedGroup.Add(frm.Pattern);
                }
            }
        }

        private void CtxProjGroupAdd_Click(object sender, EventArgs e)
        {
            Project.Add(new Group() { Name = $"Group {Project.Content.Count().ToString()}", Project = Project });
        }

        private void GroupAdded(object sender, TransparentContainer<Group>.ElementEventArgs<Group> e)
        {
            AddNode(nodeMap[e.Element.Project], e.Element, e.Element.Name, 1);
            e.Element.ElementAdded += PatternAdded;
            e.Element.ElementRemoved += PatternRemoved;
            
            foreach (var p in e.Element.Content)
                PatternAdded(sender, new TransparentContainer<Pattern>.ElementEventArgs<Pattern>(p));
        }

        private void GroupRemoved(object sender, TransparentContainer<Group>.ElementEventArgs<Group> e)
        {
            foreach (var p in e.Element.Content)
                PatternRemoved(sender, new TransparentContainer<Pattern>.ElementEventArgs<Pattern>(p));
            Nodes.Remove(nodeMap[e.Element]);
            e.Element.ElementAdded -= PatternAdded;
            e.Element.ElementRemoved -= PatternRemoved;
        }

        private void PatternAdded(object sender, TransparentContainer<Pattern>.ElementEventArgs<Pattern> e)
        {
            AddNode(nodeMap[e.Element.Group], e.Element, e.Element.Name, 2);
        }

        private void PatternRemoved(object sender, TransparentContainer<Pattern>.ElementEventArgs<Pattern> e)
        {
            Nodes.Remove(nodeMap[e.Element]);
        }

        private void AddNode(TreeNode parent, object tag, string name, int imageIndex)
        {
            TreeNode node;
            if (parent != null)
                node = parent.Nodes.Add(name, name, imageIndex, imageIndex);
            else
                node = this.Nodes.Add(name);
            node.ImageIndex = imageIndex;
            node.Tag = tag;
            nodeMap[tag] = node;
        }

        private void _Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ctxProjGroupAdd.Enabled = SelectedNodeType == NodeType.Project; 
            ctxProjGroupRemove.Enabled = SelectedNodeType == ProjectView.NodeType.Group;
            ctxProjPatternAdd.Enabled = SelectedNodeType == NodeType.Group;
            ctxProjPatternRemove.Enabled = SelectedNodeType == ProjectView.NodeType.Pattern;

            if (SelectedNodeType == ProjectView.NodeType.None)
                e.Cancel = true;
        }

        private void _AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            switch (SelectedNodeType)
            {
                case ProjectView.NodeType.Project:
                    SelectedProject.Name = e.Label;
                    break;
                case ProjectView.NodeType.Group:
                    SelectedGroup.Name = e.Label;
                    break;
                case ProjectView.NodeType.Pattern:
                    SelectedPattern.Name = e.Label;
                    break;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctxProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxProjGroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxProjGroupRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxProjPatternAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxProjPatternRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxProjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxProjects
            // 
            this.ctxProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxProjGroupAdd,
            this.ctxProjGroupRemove,
            this.toolStripSeparator4,
            this.ctxProjPatternAdd,
            this.ctxProjPatternRemove});
            this.ctxProjects.Name = "ctxProjects";
            this.ctxProjects.Size = new System.Drawing.Size(159, 98);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(155, 6);
            // 
            // ctxProjGroupAdd
            // 
            this.ctxProjGroupAdd.Image = global::PatternScanner.Properties.Resources.ico_group_add;
            this.ctxProjGroupAdd.Name = "ctxProjGroupAdd";
            this.ctxProjGroupAdd.Size = new System.Drawing.Size(158, 22);
            this.ctxProjGroupAdd.Text = "Add group";
            // 
            // ctxProjGroupRemove
            // 
            this.ctxProjGroupRemove.Image = global::PatternScanner.Properties.Resources.ico_group_remove;
            this.ctxProjGroupRemove.Name = "ctxProjGroupRemove";
            this.ctxProjGroupRemove.Size = new System.Drawing.Size(158, 22);
            this.ctxProjGroupRemove.Text = "Remove group";
            // 
            // ctxProjPatternAdd
            // 
            this.ctxProjPatternAdd.Image = global::PatternScanner.Properties.Resources.ico_pattern_add;
            this.ctxProjPatternAdd.Name = "ctxProjPatternAdd";
            this.ctxProjPatternAdd.Size = new System.Drawing.Size(158, 22);
            this.ctxProjPatternAdd.Text = "Add pattern";
            // 
            // ctxProjPatternRemove
            // 
            this.ctxProjPatternRemove.Image = global::PatternScanner.Properties.Resources.ico_pattern_remove;
            this.ctxProjPatternRemove.Name = "ctxProjPatternRemove";
            this.ctxProjPatternRemove.Size = new System.Drawing.Size(158, 22);
            this.ctxProjPatternRemove.Text = "Remove pattern";
            // 
            // ProjectView
            // 
            this.ContextMenuStrip = this.ctxProjects;
            this.LineColor = System.Drawing.Color.Black;
            this.ctxProjects.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
