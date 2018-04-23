using DevExpress.Accessibility;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;

namespace CustomEdit
{
    [UserRepositoryItem("Register")] 
    public class RepositoryItemCustomCheckedComboBoxEdit : RepositoryItemCheckedComboBoxEdit
    {
        CheckStyles _checkStyle = CheckStyles.Standard;
        public CheckStyles CheckStyle { 
            get { return _checkStyle; }
            set
            {
                if (_checkStyle != value)
                {
                    _checkStyle = value;
                    OnPropertiesChanged();
                }                    
            }
        }
        internal const string EditorName = "CustomCheckedComboBoxEdit";
        
        static RepositoryItemCustomCheckedComboBoxEdit()
        {
            Register();
        }

        public RepositoryItemCustomCheckedComboBoxEdit() {}

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(CustomCheckedComboBoxEdit), typeof(RepositoryItemCustomCheckedComboBoxEdit),
                typeof(PopupContainerEditViewInfo), new ButtonEditPainter(), true, null, typeof(PopupEditAccessible)));
        }

        public override string EditorTypeName
        {
            get
            {
                return EditorName;
            }
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemCustomCheckedComboBoxEdit source = item as RepositoryItemCustomCheckedComboBoxEdit;
                if (source == null) return;
                CheckStyle = source.CheckStyle;
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}
