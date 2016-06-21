using System;
using System.Windows.Forms;

namespace ChocoMon
{
    class ChocoMonController
    {

        private readonly NotifyIcon notifyIcon;

        public ChocoMonController(NotifyIcon notifyIcon)
        {
            this.notifyIcon = notifyIcon;
        }

        # region context menu creation

        public void BuildContextMenu(ContextMenuStrip contextMenuStrip)
        {
            contextMenuStrip.Items.Clear();
        }

        private ToolStripMenuItem BuildSubMenu(string project)
        {
            var menuItem = new ToolStripMenuItem(project);
            return menuItem;
        }

        #endregion context menu creation

        # region support methods

        private ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, int enabledCount, int disabledCount, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null) { item.Click += eventHandler; }
            /*
            item.Image = (enabledCount > 0 && disabledCount > 0) ? Properties.Resources.signal_yellow
                         : (enabledCount > 0) ? Properties.Resources.signal_green
                         : (disabledCount > 0) ? Properties.Resources.signal_red
                         : null;
                         */
            item.ToolTipText = (enabledCount > 0 && disabledCount > 0) ?
                                                 string.Format("{0} enabled, {1} disabled", enabledCount, disabledCount)
                         : (enabledCount > 0) ? string.Format("{0} enabled", enabledCount)
                         : (disabledCount > 0) ? string.Format("{0} disabled", disabledCount)
                         : "";
            return item;
        }
        public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            return ToolStripMenuItemWithHandler(displayText, 0, 0, eventHandler);
        }

        #endregion support methods
    }
}
