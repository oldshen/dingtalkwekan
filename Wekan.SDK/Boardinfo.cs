using System;
namespace Wekan.SDK
{
    public class Boardinfo
    {
        public string Title { get; set; }

        public BoardMember[] Members { get; set; }
    }

    public class BoardMember
    {
        public string Userid { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }
    }
}
