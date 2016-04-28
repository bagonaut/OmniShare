using Microsoft.WindowsAzure.Mobile.Service;

namespace OmniShare.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }

    public class ClipboardContent : EntityData
    {
        
        public System.DateTime TimeStamp { get; set; }

        //TODO: Multiple types and type marshaling
        public string Contents;
    }

}