using umbraco.interfaces;

namespace W3S_GCS.Models.Tree {
    public class PropertiesActionModel : IAction {
        private static readonly PropertiesActionModel m_instance = new PropertiesActionModel();

        public static PropertiesActionModel Instance {
            get { return m_instance; }
        }

        #region IAction Members

        public char Letter {
            get {
                return 'ö';
            }
        }

        public string JsFunctionName {
            get {
                return null;
            }
        }

        public string JsSource {
            get {
                return "function OpenSortWindow(){ var node = UmbClientMgr.mainTree().getActionNode();UmbClientMgr.openModalWindow('/Umbraco/Dialogs/RaceNodeSort.aspx?id='+ node.nodeId, 'Sort items', true, 350, 380); }"; ;
            }
        }

        public string Alias {
            get {
                return "w3s_gcs";
            }
        }

        public string Icon {
            get {
                return null;
            }
        }

        public bool ShowInNotifier {
            get {
                return false;
            }
        }
        public bool CanBePermissionAssigned {
            get {
                return false;
            }
        }
        #endregion
    }
}