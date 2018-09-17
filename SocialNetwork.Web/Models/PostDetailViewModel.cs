using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class PostDetailViewModel
    {
        public string Content { get; set; }

        public string UserName { get; set; }
        public IEnumerable<CommentListItemViewModel> Comments { get; set; }
    }
}