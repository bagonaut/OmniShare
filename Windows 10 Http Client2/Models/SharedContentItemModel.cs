using System;

namespace OmniShareUWP.Models
{
    public class SharedContenItemtModel
    {
        //TODO: Change to GUID
        public int Id { get; set; }

        public string Title { get; set; }
        //TODO: Change type to 
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
