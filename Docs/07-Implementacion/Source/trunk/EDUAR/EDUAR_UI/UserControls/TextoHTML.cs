using AjaxControlToolkit.HTMLEditor;

namespace EDUAR_UI.UserControls
{
    public class TextoHTML : Editor
    {
        protected override void FillBottomToolbar()
        {
            //Do not add any buttons in the bottom toolbar
            //BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
        }
    }
}