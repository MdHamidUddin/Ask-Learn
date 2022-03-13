using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Moderator
{
    public class CommentModel
    {
        public int cid { get; set; }
        public int pid { get; set; }
        public int uid { get; set; }
        public byte[] comment1 { get; set; }
        public Nullable<int> upVote { get; set; }
        public Nullable<int> downVote { get; set; }
        public System.DateTime dateTime { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}