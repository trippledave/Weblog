using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class AddCommentModel
    {
        public int ID { get; set; }
        [DisplayName("Text")]
        [Required]
        public string Text { get; set; }
        public String Author { get; set; }
        public int EntryID { get; set; }
        [DisplayName("Ergebnis des Captchas")]
        public int CaptchaResult { get; set; }

        private void UpdateModel(Comment source)
        {
            this.ID = source.CommentID;
            this.Text = source.Text;
            if (source.User.DisplayName != null)
            {
                this.Author = source.User.DisplayName;
            }
            else
            {
                this.Author = source.User.UserName;
            }

        }

        public void UpdateSource(Comment source)
        {
            source.Text = this.Text;
            source.EntryID = this.EntryID;
        }


        public AddCommentModel()
        {

        }

        public AddCommentModel(Comment source)
        {
            UpdateModel(source);
        }
    }
}