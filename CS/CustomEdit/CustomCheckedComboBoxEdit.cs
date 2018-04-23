using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomEdit
{
    public class CustomCheckedComboBoxEdit : CheckedComboBoxEdit
    {
        CheckedListBoxControl listBoxControl;
        ImageCollection images;

        static CustomCheckedComboBoxEdit()
        { RepositoryItemCustomCheckedComboBoxEdit.Register(); }

        public CustomCheckedComboBoxEdit() {}

        ~CustomCheckedComboBoxEdit()
        {
            if (listBoxControl != null)
                listBoxControl.DrawItem -= CustomCheckedComboBoxEdit_DrawItem;
            images.Dispose();
        }

        public override string EditorTypeName
        {
            get {return RepositoryItemCustomCheckedComboBoxEdit.EditorName;}
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomCheckedComboBoxEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomCheckedComboBoxEdit; }
        }

        protected override PopupContainerControl CreatePopupCheckListControl()
        {
            PopupContainerControl containerControl = base.CreatePopupCheckListControl();
            SetControl(containerControl);
            return containerControl;            
        }

        private void SetControl(PopupContainerControl containerControl)
        {
            foreach (Control con in containerControl.Controls)
            {
                if (con.GetType().BaseType == typeof(CheckedListBoxControl))
                {                   
                    listBoxControl = con as CheckedListBoxControl;
                    listBoxControl.DrawItem += CustomCheckedComboBoxEdit_DrawItem;
                    return;
                }
            }
        }

        void CustomCheckedComboBoxEdit_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (listBoxControl != null && Properties.CheckStyle != DevExpress.XtraEditors.Controls.CheckStyles.Standard)
            {
                CheckedListBoxControl lbControl = sender as CheckedListBoxControl;
                CheckedListBoxViewInfo vi = lbControl.GetViewInfo() as CheckedListBoxViewInfo;
                CheckedListBoxViewInfo.CheckedItemInfo checkItemInfo = vi.GetItemByIndex(e.Index) as CheckedListBoxViewInfo.CheckedItemInfo;
                checkItemInfo.CheckArgs.CheckStyle = Properties.CheckStyle;
                if (Properties.CheckStyle == DevExpress.XtraEditors.Controls.CheckStyles.Radio)                
                    return;                                
                SetProperties(checkItemInfo);
            }
        }

        protected void SetProperties(CheckedListBoxViewInfo.CheckedItemInfo checkItemInfo)
        {
            CheckObjectInfoArgs e = checkItemInfo.CheckArgs;
            if (e.DefaultImages == null)
                e.DefaultImages = LoadImages();

            int style = (int)e.CheckStyle;
            style = style * 4;
            if (style > e.DefaultImages.Count - 1) return;
            e.PictureUnchecked = e.DefaultImages[style] as Bitmap;
            e.PictureChecked = e.DefaultImages[style + 1] as Bitmap;
            e.PictureGrayed = e.DefaultImages[style + 3] as Bitmap;
        }

        protected ArrayList LoadImages()
        {
            if (images == null)
            {
                images = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.XtraEditors.Images.CheckBoxes.gif", typeof(RepositoryItemCheckEdit).Assembly, new Size(18, 18), Color.Magenta); 
            }
            return new ArrayList(images.Images);
        }
    }
}
